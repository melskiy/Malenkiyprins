using Hwdtech;
using Hwdtech.Ioc;
using System.Collections.Concurrent;

namespace SpaceBattle.Lib.Test;

public class SendCommandTest
{

    ConcurrentDictionary<int, ServerThread> mapServerThreads = new ConcurrentDictionary<int, ServerThread>();
    ConcurrentDictionary<int, ISender> mapServerThreadsSenders = new ConcurrentDictionary<int, ISender>();

    public SendCommandTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "ThreadMap", (object[] args) => mapServerThreads).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SenderMap", (object[] args) => mapServerThreadsSenders).Execute();
    }
    [Fact]
    public void UnsuccessfullSendCommandTestThrowsException()
    {
        var key = 1;
        var falseKey = 3;

        var mre = new AutoResetEvent(true);

        IStrategy createAndStartSTStrategy = new CreateAndStartThreadStrategy();

        var c = (ICommand)createAndStartSTStrategy.DoAlgorithm(key, () =>
        {
            mre.WaitOne();
        });
        c.Execute();

        var sendStrategy = new SendCommandStrategy();

        var c1 = (ICommand)sendStrategy.DoAlgorithm(falseKey, new ActionCommand(() =>
        {
            mre.WaitOne();
        }));


        Assert.Throws<Exception>(() =>
        {
            c1.Execute();
            mre.Set();
            Thread.Sleep(1000);
        });

        var hardStopStrategy = new HardStopServerThreadCommandStrategy();

        var hs = (ICommand)hardStopStrategy.DoAlgorithm(key);

        hs.Execute();
    }
}
