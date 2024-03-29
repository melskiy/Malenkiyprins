using Hwdtech;
using Hwdtech.Ioc;
using System.Collections.Concurrent;

namespace SpaceBattle.Lib.Test;

public class SoftStopTest
{

    ConcurrentDictionary<string, ServerThread> threadMap = new ConcurrentDictionary<string, ServerThread>();
    ConcurrentDictionary<string, ISender> senderMap = new ConcurrentDictionary<string, ISender>();

    public SoftStopTest()
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
    public void SuccessfulSoftStopCommand()
    {
        var id = "5";
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
        var id = "6";
        var ssFlag = false;

        IStrategy createAndStartSTStrategy = new CreateAndStartThreadStrategy();

        var c = (ICommand)createAndStartSTStrategy.DoAlgorithm(id);
        c.Execute();

        var serverThread = threadMap[id];
        var ss = new SoftStopServerThreadCommand(serverThread, () =>
        {
            ssFlag = true;
        });

        Assert.Throws<Exception>(() =>
        {
            ss.Execute();
        });


        Assert.False(ssFlag);

        var hardStopStrategy = new HardStopServerThreadCommandStrategy();

        var hs = (ICommand)hardStopStrategy.DoAlgorithm(id);

        hs.Execute();
    }

    [Fact]
    public void UnsuccessfulSoftStopServerThreadStrategyThrowsException()
    {
        var id = "1";
        var falseid = "4";

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
    public void SuccessfulSoftStopCommandWithOtherCommands()
    {
        var id = "9";
        var isExecute = false;


        var cv = new AutoResetEvent(false);

        ICommand createTCommand = new CreateThreadCommand(id);
        createTCommand.Execute();

        var softStopStrategy = new SoftStopServerThreadCommandStrategy();
        var sendStrategy = new SendCommandStrategy();

        var ss = (ICommand)softStopStrategy.DoAlgorithm(id);
        ss.Execute();

        var c = (ICommand)sendStrategy.DoAlgorithm(id, new ActionCommand(() =>
        {
            isExecute = true;
            cv.Set();
        }));

        c.Execute();

        ICommand startTCommand = new StartThreadCommand(id);
        startTCommand.Execute();

        cv.WaitOne();

        Assert.True(isExecute);
    }
}
