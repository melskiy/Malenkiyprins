using Hwdtech;
using Hwdtech.Ioc;
using System.Collections.Concurrent;

namespace SpaceBattle.Lib.Test;

public class SoftStopTest
{

    ConcurrentDictionary<int, ServerThread> threadMap = new ConcurrentDictionary<int, ServerThread>();
    ConcurrentDictionary<int, ISender> senderMap = new ConcurrentDictionary<int, ISender>();

    public SoftStopTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "ThreadMap", (object[] args) => threadMap).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SenderMap", (object[] args) => senderMap).Execute();
    }

    [Fact]
    public void SuccessfulSoftStopCommand()
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
    public void UnsuccessfulStopStopServerThreadStrategyThrowsException()
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
    public void SuccessfulSoftStopCommandWithOtherCommands()
    {
        var id = 9;
        var isExecute = false;


        var cv = new AutoResetEvent(false);

        ICommand createTCommand = new CreateThreadCommand(id);
        createTCommand.Execute();

        var softStopStrategy = new SoftStopServerThreadCommandStrategy();
        var sendStrategy = new SendCommandStrategy();

        var ss = (ICommand)softStopStrategy.DoAlgorithm(id);
        ss.Execute();

        var c = (ICommand)sendStrategy.DoAlgorithm(id, new ActionCommand((object[] args) =>
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
