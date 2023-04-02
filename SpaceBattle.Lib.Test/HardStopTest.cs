using Hwdtech;
using Hwdtech.Ioc;
using System.Collections.Concurrent;

namespace SpaceBattle.Lib.Test;

public class HardStopTest
{
    ConcurrentDictionary<string, ServerThread> threadMap = new ConcurrentDictionary<string, ServerThread>();
    ConcurrentDictionary<string, ISender> senderMap = new ConcurrentDictionary<string, ISender>();

    public HardStopTest()
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
    public void UnsuccessfulHardStopServerThreadStrategyDoAldorithmThrowsException()
    {
        var id = "1";
        var falseid = "4";

        IStrategy createAndStartSTStrategy = new CreateAndStartThreadStrategy();

        var c = (ICommand)createAndStartSTStrategy.DoAlgorithm(id);
        c.Execute();

        var hardStopStrategy = new HardStopServerThreadCommandStrategy();

        Assert.Throws<Exception>(() =>
        {
            var hs = (ICommand)hardStopStrategy.DoAlgorithm(falseid);
        });

        var hs = (ICommand)hardStopStrategy.DoAlgorithm(id);

        hs.Execute();
    }

    [Fact]
    public void UnsuccessfulHardStopServerThreadCommandExecuteThrowsException()
    {
        var id = "5";

        IStrategy createAndStartSTStrategy = new CreateAndStartThreadStrategy();

        var c = (ICommand)createAndStartSTStrategy.DoAlgorithm(id);
        c.Execute();

        var serverThread = threadMap[id];

        var hs = new HardStopServerThreadCommand(serverThread);

        Assert.Throws<Exception>(() => hs.Execute());

        var hardStopStrategy = new HardStopServerThreadCommandStrategy();

        var hs2 = (ICommand)hardStopStrategy.DoAlgorithm(id);

        hs2.Execute();
    }
}
