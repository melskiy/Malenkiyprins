using Hwdtech;
using Hwdtech.Ioc;
using System.Collections.Concurrent;

namespace SpaceBattle.Lib.Test;

public class SoftStopTest
{

    ConcurrentDictionary<int, ServerThread> mapServerThreads = new ConcurrentDictionary<int, ServerThread>();
    ConcurrentDictionary<int, ISender> mapServerThreadsSenders = new ConcurrentDictionary<int, ISender>();

    public SoftStopTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "ThreadMap", (object[] args) => mapServerThreads).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SenderMap", (object[] args) => mapServerThreadsSenders).Execute();
    }

    [Fact]
    public void SuccessfulSoftStopCommandTest()
    {
        var key = 5;
        var ssFlag = false;
        var mre = new AutoResetEvent(true);

        IStrategy createAndStartSTStrategy = new CreateAndStartThreadStrategy();

        var c = (ICommand)createAndStartSTStrategy.DoAlgorithm(key, () =>
        {
            mre.WaitOne();
        });

        c.Execute();

        var softStopStrategy = new SoftStopServerThreadCommandStrategy();

        var ss = (ICommand)softStopStrategy.DoAlgorithm(key, () =>
        {
            ssFlag = true;
            mre.WaitOne();
        });

        ss.Execute();

        mre.Set();
        Thread.Sleep(1000);

        Assert.True(ssFlag);
    }

    [Fact]
    public void UnsuccessfulSoftStopCommandStrategyThrowException()
    {
        var key = 6;
        var ssFlag = false;
        var mre = new AutoResetEvent(true);

        IStrategy createAndStartSTStrategy = new CreateAndStartThreadStrategy();

        var c = (ICommand)createAndStartSTStrategy.DoAlgorithm(key, () =>
        {
            mre.WaitOne();
        });
        c.Execute();

        var serverThread = mapServerThreads[key];
        var ss = new SoftStopServerThreadCommand(serverThread, () => { ssFlag = true; });

        Assert.Throws<Exception>(() => ss.Execute());
        Assert.False(ssFlag);

        var hardStopStrategy = new HardStopServerThreadCommandStrategy();

        var hs = (ICommand)hardStopStrategy.DoAlgorithm(key);

        hs.Execute();
    }

    [Fact]
    public void UnsuccessfulHardStopServerThreadStrategyTestThrowsException()
    {
        var key = 1;
        var falseKey = 4;

        var mre = new AutoResetEvent(true);

        IStrategy createAndStartSTStrategy = new CreateAndStartThreadStrategy();

        var c = (ICommand)createAndStartSTStrategy.DoAlgorithm(key, () =>
        {
            mre.WaitOne();
        });
        c.Execute();

        var softStopStrategy = new SoftStopServerThreadCommandStrategy();

        Assert.Throws<Exception>(() =>
        {
            var ss = (ICommand)softStopStrategy.DoAlgorithm(falseKey);
        });

        var hardStopStrategy = new HardStopServerThreadCommandStrategy();

        var hs = (ICommand)hardStopStrategy.DoAlgorithm(key);

        hs.Execute();
    }
    [Fact]
    public void SuccessfulSoftStopCommandTestWithOtherCommands()
    {
        var key = 9;
        var isExecute = false;


        var mre = new AutoResetEvent(true);

        IStrategy createAndStartSTStrategy = new CreateAndStartThreadStrategy();

        var c = (ICommand)createAndStartSTStrategy.DoAlgorithm(key, () =>
        {
            mre.WaitOne();
        });

        c.Execute();
        
        var softStopStrategy = new SoftStopServerThreadCommandStrategy();
        var sendStrategy = new SendCommandStrategy();

        var ss = (ICommand)softStopStrategy.DoAlgorithm(key);

        ss.Execute();

        var c2 = (ICommand)sendStrategy.DoAlgorithm(key, new ActionCommand(() =>
        {
            isExecute = true;
            mre.WaitOne();
        }));

        c2.Execute();

        mre.Set();
        Thread.Sleep(1000);

        Assert.True(isExecute);
    }
}
