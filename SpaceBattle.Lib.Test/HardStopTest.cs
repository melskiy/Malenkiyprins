using Hwdtech;
using Hwdtech.Ioc;
using System.Collections.Concurrent;

namespace SpaceBattle.Lib.Test;

public class HardStopTest
{
    ConcurrentDictionary<string, ServerThread> mapServerThreads = new ConcurrentDictionary<string, ServerThread>();
    ConcurrentDictionary<string, ISender> mapServerThreadsSenders = new ConcurrentDictionary<string, ISender>();

    public HardStopTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "ThreadMap", (object[] args) => mapServerThreads).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SenderMap", (object[] args) => mapServerThreadsSenders).Execute();
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

        var serverThread = mapServerThreads[id];

        var hs = new HardStopServerThreadCommand(serverThread);

        Assert.Throws<Exception>(() => hs.Execute());

        var hardStopStrategy = new HardStopServerThreadCommandStrategy();

        var hs2 = (ICommand)hardStopStrategy.DoAlgorithm(id);

        hs2.Execute();
    }
}
