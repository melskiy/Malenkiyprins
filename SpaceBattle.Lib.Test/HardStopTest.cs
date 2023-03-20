using Hwdtech;
using Hwdtech.Ioc;
using System.Collections.Concurrent;

namespace SpaceBattle.Lib.Test;

public class HardStopTest
{
    ConcurrentDictionary<int, ServerThread> mapServerThreads = new ConcurrentDictionary<int, ServerThread>();
    ConcurrentDictionary<int, ISender> mapServerThreadsSenders = new ConcurrentDictionary<int, ISender>();

    public HardStopTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "ThreadMap", (object[] args) => mapServerThreads).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SenderMap", (object[] args) => mapServerThreadsSenders).Execute();
    }
    [Fact]
    public void UnsuccessfulHardStopServerThreadStrategyTestThrowsExceptionFromConstructor()
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

        var hardStopStrategy = new HardStopServerThreadCommandStrategy();

        Assert.Throws<Exception>(() =>
        {
            var hs = (ICommand)hardStopStrategy.DoAlgorithm(falseKey);
        });

        var hs = (ICommand)hardStopStrategy.DoAlgorithm(key);

        hs.Execute();
    }

    [Fact]
    public void UnsuccessfulHardStopServerThreadCommandTestThrowsExceptionFromExecute()
    {
        var key = 5;
        var mre = new AutoResetEvent(true);

        IStrategy createAndStartSTStrategy = new CreateAndStartThreadStrategy();

        var c = (ICommand)createAndStartSTStrategy.DoAlgorithm(key, ()=> { 
            mre.WaitOne();
        });
        c.Execute();

        var serverThread = mapServerThreads[key];
        var hs = new HardStopServerThreadCommand(serverThread);

        Assert.Throws<Exception>(() => hs.Execute());


        var hardStopStrategy = new HardStopServerThreadCommandStrategy();

        var hs2 = (ICommand)hardStopStrategy.DoAlgorithm(key);

        hs2.Execute();
    }
}
