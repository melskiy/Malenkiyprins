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
        var id = 5;
        var ssFlag = false;
        var cv = new AutoResetEvent(false);

        IStrategy createAndStartSTStrategy = new CreateAndStartThreadStrategy();

        var c = (ICommand)createAndStartSTStrategy.DoAlgorithm(id);

        c.Execute();

        var softStopStrategy = new SoftStopServerThreadCommandStrategy();

        var ss = (ICommand)softStopStrategy.DoAlgorithm(id, () =>
        {
            ssFlag = true;
            cv.Set();
        });

        ss.Execute();

        cv.WaitOne();

        Assert.True(ssFlag);
    }

    [Fact]
    public void UnsuccessfulSoftStopCommandStrategyThrowException()
    {
        var id = 6;
        var ssFlag = false;
        var cv = new AutoResetEvent(false);

        IStrategy createAndStartSTStrategy = new CreateAndStartThreadStrategy();

        var c = (ICommand)createAndStartSTStrategy.DoAlgorithm(id);
        c.Execute();

        var serverThread = mapServerThreads[id];
        var ss = new SoftStopServerThreadCommand(serverThread, () => { 
            ssFlag = true; 
            cv.Set();
        });
        
        Assert.Throws<Exception>(() => {
            ss.Execute(); 
            cv.WaitOne();
        });

        Assert.False(ssFlag);

        var hardStopStrategy = new HardStopServerThreadCommandStrategy();

        var hs = (ICommand)hardStopStrategy.DoAlgorithm(id);

        hs.Execute();
    }

    [Fact]
    public void UnsuccessfulHardStopServerThreadStrategyTestThrowsException()
    {
        var id = 1;
        var falseid = 4;

        IStrategy createAndStartSTStrategy = new CreateAndStartThreadStrategy();

        var c = (ICommand)createAndStartSTStrategy.DoAlgorithm(id);
        c.Execute();

        var softStopStrategy = new SoftStopServerThreadCommandStrategy();

        Assert.Throws<Exception>(() =>
        {
            var ss = (ICommand)softStopStrategy.DoAlgorithm(falseid);
        });

        var hardStopStrategy = new HardStopServerThreadCommandStrategy();

        var hs = (ICommand)hardStopStrategy.DoAlgorithm(id);

        hs.Execute();
    }
    [Fact]
    public void SuccessfulSoftStopCommandTestWithOtherCommands()
    {
        var id = 9;
        var isExecute = false;


        var cv = new AutoResetEvent(false);

        IStrategy createAndStartSTStrategy = new CreateAndStartThreadStrategy();

        var c = (ICommand)createAndStartSTStrategy.DoAlgorithm(id);

        c.Execute();
        
        var softStopStrategy = new SoftStopServerThreadCommandStrategy();
        var sendStrategy = new SendCommandStrategy();

        var ss = (ICommand)softStopStrategy.DoAlgorithm(id);

        ss.Execute();

        var c2 = (ICommand)sendStrategy.DoAlgorithm(id, new ActionCommand(() =>
        {
            isExecute = true;
            cv.Set();
        }));

        c2.Execute();

        cv.WaitOne();

        Assert.True(isExecute);
    }
}
