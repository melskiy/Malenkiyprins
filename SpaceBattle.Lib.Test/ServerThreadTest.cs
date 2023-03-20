using Hwdtech;
using Hwdtech.Ioc;
using System.Collections.Concurrent;

namespace SpaceBattle.Lib.Test;

public class ServerThreadTests
{
    ConcurrentDictionary<int, ServerThread> mapServerThreads = new ConcurrentDictionary<int, ServerThread>();
    ConcurrentDictionary<int, ISender> mapServerThreadsSenders = new ConcurrentDictionary<int, ISender>();

    public ServerThreadTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "ThreadMap", (object[] args) => mapServerThreads).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SenderMap", (object[] args) => mapServerThreadsSenders).Execute();
    }

    [Fact]
    public void SuccessfulCreateStartAndHardStopServerThreadWithoutParamsForStrategies()
    {
        var isActive = false;

        var key = 1;

        var mre = new AutoResetEvent(true);

        IStrategy createAndStartSTStrategy = new CreateAndStartThreadStrategy();

        var c = (ICommand)createAndStartSTStrategy.DoAlgorithm(key, () =>
        {
            mre.WaitOne();
        });
        c.Execute();

        var sendStrategy = new SendCommandStrategy();

        var c1 = (ICommand)sendStrategy.DoAlgorithm(key, new ActionCommand(() =>
        {
            isActive = true;
            mre.WaitOne();
        }));

        c1.Execute();

        mre.Set();
        Thread.Sleep(1000);

        Assert.True(isActive);
        Assert.True(mapServerThreads.TryGetValue(key, out ServerThread? st));
        Assert.True(mapServerThreadsSenders.TryGetValue(key, out ISender? s));

        var hardStopStrategy = new HardStopServerThreadCommandStrategy();

        var hs = (ICommand)hardStopStrategy.DoAlgorithm(key);

        hs.Execute();
    }

    [Fact]
    public void SuccessfulCreateStartAndHardStopServerThreadWithParamsForStrategies()
    {
        var isActive = false;
        var createAndStartFlag = false;
        var hsFlag = false;

        var key = 2;
        var mre = new AutoResetEvent(true);

        IStrategy createAndStartSTStrategy = new CreateAndStartThreadStrategy();

        var c = (ICommand)createAndStartSTStrategy.DoAlgorithm(key, () =>
        {
            createAndStartFlag = true;
            mre.WaitOne();
        });
        c.Execute();

        var sendStrategy = new SendCommandStrategy();

        var c1 = (ICommand)sendStrategy.DoAlgorithm(key, new ActionCommand(() =>
        {
            isActive = true;
            mre.WaitOne();
        }));

        c1.Execute();

        mre.Set();
        Thread.Sleep(1000);

        Assert.True(isActive);
        Assert.True(mapServerThreads.TryGetValue(key, out ServerThread? st));
        Assert.True(mapServerThreadsSenders.TryGetValue(key, out ISender? s));
        Assert.True(createAndStartFlag);

        var hardStopStrategy = new HardStopServerThreadCommandStrategy();

        var hs = (ICommand)hardStopStrategy.DoAlgorithm(key, () =>
        {
            hsFlag = true;
            mre.WaitOne();
        });

        hs.Execute();

        mre.Set();
        Thread.Sleep(1000);

        Assert.True(hsFlag);
    }

    [Fact]
    public void ServerThreadGetHashCodeTest()
    {
        var queue1 = new BlockingCollection<ICommand>();
        var serverThread1 = new ServerThread(new ReceiverAdapter(queue1));
        var queue2 = new BlockingCollection<ICommand>();
        var serverThread2 = new ServerThread(new ReceiverAdapter(queue2));
        Assert.True(serverThread1.GetHashCode() != serverThread2.GetHashCode());
    }

    [Fact]
    public void ServerThreadEqualsIsNotThreadTest()
    {
        var queue1 = new BlockingCollection<ICommand>();
        var serverThread1 = new ServerThread(new ReceiverAdapter(queue1));
        Assert.False(serverThread1.Equals(2));
    }

    [Fact]
    public void ServerThreadEqualsTest()
    {
        var queue1 = new BlockingCollection<ICommand>();
        var serverThread1 = new ServerThread(new ReceiverAdapter(queue1));
        Assert.False(serverThread1.Equals(Thread.CurrentThread));
    }

    [Fact]
    public void ServerThreadOperatorEqualsTest()
    {
        var queue1 = new BlockingCollection<ICommand>();
        var serverThread1 = new ServerThread(new ReceiverAdapter(queue1));
        Assert.False(serverThread1 == Thread.CurrentThread);
    }

    [Fact]
    public void FindHandlerExceptionForServerThread()
    {
        var handleFlag = false;


        var cmd = new Mock<ICommand>();
        cmd.Setup(c => c.Execute()).Throws<Exception>();

        var key = 11;
        var mre = new AutoResetEvent(true);

        IStrategy createAndStartSTStrategy = new CreateAndStartThreadStrategy();

        var c = (ICommand)createAndStartSTStrategy.DoAlgorithm(key, () =>
        {
            mre.WaitOne();
        });

        c.Execute();

        var sendStrategy = new SendCommandStrategy();

        var c1 = (ICommand)sendStrategy.DoAlgorithm(key, new ActionCommand(() =>
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
            IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
            var handler = new Mock<ICommand>();
            handler.Setup(c => c.Execute()).Callback(() => mre.WaitOne());
            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "FindHandlerStrategy", (object[] args) => handler.Object).Execute();
            handleFlag = true;
            throw new Exception();

        }));

        c1.Execute();

        mre.Set();
        Thread.Sleep(1000);

        Assert.True(handleFlag);

        var hardStopStrategy = new HardStopServerThreadCommandStrategy();

        var hs = (ICommand)hardStopStrategy.DoAlgorithm(key);

        hs.Execute();
    }
}
