using Hwdtech;
using Hwdtech.Ioc;
using System.Collections.Concurrent;

namespace SpaceBattle.Lib.Test;

public class ServerThreadTest
{
    ConcurrentDictionary<string, ServerThread> threadMap = new ConcurrentDictionary<string, ServerThread>();
    ConcurrentDictionary<string, ISender> senderMap = new ConcurrentDictionary<string, ISender>();

    public ServerThreadTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        var gtftm = new GetThreadFromThreadMapStrategy();
        var gsfsm = new GetSenderFromSenderMapStrategy();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "ThreadMap", (object[] args) => threadMap).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SenderMap", (object[] args) => senderMap).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetThreadFromThreadMap", (object[] args) => gtftm.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetSenderFromSenderMap", (object[] args) => gsfsm.DoAlgorithm(args)).Execute();
    }

    [Fact]
    public void SuccessfulCreateStartServerThreadWithoutParamsForStrategies()
    {
        var isActive = false;

        var id = "1";

        var cv = new AutoResetEvent(false);

        IStrategy createAndStartSTStrategy = new CreateAndStartThreadStrategy();

        var c = (ICommand)createAndStartSTStrategy.DoAlgorithm(id);

        c.Execute();

        var sendStrategy = new SendCommandStrategy();

        var c1 = (ICommand)sendStrategy.DoAlgorithm(id, new ActionCommand(() =>
        {   
            isActive = true;
            cv.Set();
        }));

        c1.Execute();

        cv.WaitOne();

        Assert.True(isActive);
        Assert.True(threadMap.TryGetValue(id, out ServerThread? st));
        Assert.True(senderMap.TryGetValue(id, out ISender? s));

        var hardStopStrategy = new HardStopServerThreadCommandStrategy();

        var hs = (ICommand)hardStopStrategy.DoAlgorithm(id);

        hs.Execute();
    }

    [Fact]
    public void SuccessfulCreateStartAndHardStopServerThreadWithParamsForStrategies()
    {
        var isActive = false;
        var createAndStartFlag = false;
        var hsFlag = false;

        var id = "2";
        var cv = new AutoResetEvent(false);

        IStrategy createAndStartSTStrategy = new CreateAndStartThreadStrategy();

        var c = (ICommand)createAndStartSTStrategy.DoAlgorithm(id, () =>
        {
            createAndStartFlag = true;
        });
        c.Execute();

        var sendStrategy = new SendCommandStrategy();

        var c1 = (ICommand)sendStrategy.DoAlgorithm(id, new ActionCommand(() =>
        {
            isActive = true;
            cv.Set();
        }));

        c1.Execute();

        cv.WaitOne();

        Assert.True(isActive);
        Assert.True(threadMap.TryGetValue(id, out ServerThread? st));
        Assert.True(senderMap.TryGetValue(id, out ISender? s));
        Assert.True(createAndStartFlag);

        var hardStopStrategy = new HardStopServerThreadCommandStrategy();

        var hs = (ICommand)hardStopStrategy.DoAlgorithm(id, () =>
        {
            hsFlag = true;
            cv.Set();
        });

        hs.Execute();

        cv.WaitOne();

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
        cmd.Setup(c => c.Execute()).Callback(() => throw new Exception());

        var id = "11";
        var cv = new AutoResetEvent(false);

        IStrategy createAndStartSTStrategy = new CreateAndStartThreadStrategy();


        var c = (ICommand)createAndStartSTStrategy.DoAlgorithm(id);

        c.Execute();

        var sendStrategy = new SendCommandStrategy();

        var c1 = (ICommand)sendStrategy.DoAlgorithm(id, new ActionCommand(() =>
        {   
            new InitScopeBasedIoCImplementationCommand().Execute();
            IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
            var gtftm = new GetThreadFromThreadMapStrategy();
            var gsfsm = new GetSenderFromSenderMapStrategy();
            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "ThreadMap", (object[] args) => threadMap).Execute();
            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SenderMap", (object[] args) => senderMap).Execute();
            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetThreadFromThreadMap", (object[] args) => gtftm.DoAlgorithm(args)).Execute();
            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetSenderFromSenderMap", (object[] args) => gsfsm.DoAlgorithm(args)).Execute();
            var handler = new Mock<ICommand>();
            handler.Setup(h => h.Execute()).Callback(() =>  cv.Set());
            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "FindHandlerStrategy", (object[] args) => handler.Object).Execute();
            handleFlag = true;
            cmd.Object.Execute();
        }));

        c1.Execute();

        cv.WaitOne();

        Assert.True(handleFlag);

        var hardStopStrategy = new HardStopServerThreadCommandStrategy();

        var hs = (ICommand)hardStopStrategy.DoAlgorithm(id);

        hs.Execute();
    }
}
