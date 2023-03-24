using Hwdtech;
using Hwdtech.Ioc;
using System.Collections.Concurrent;

namespace SpaceBattle.Lib.Test;

public class SendCommandTest
{

    ConcurrentDictionary<int, ServerThread> thradMap = new ConcurrentDictionary<int, ServerThread>();
    ConcurrentDictionary<int, ISender> senderMap = new ConcurrentDictionary<int, ISender>();

    public SendCommandTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "ThreadMap", (object[] args) => thradMap).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SenderMap", (object[] args) => senderMap).Execute();
    }
    [Fact]
    public void UnsuccessfullSendCommandTestThrowsException()
    {
        var id = 1;
        var falseid = 3;

        IStrategy createAndStartSTStrategy = new CreateAndStartThreadStrategy();

        var c = (ICommand)createAndStartSTStrategy.DoAlgorithm(id);
        c.Execute();

        var sendStrategy = new SendCommandStrategy();

        var c1 = (ICommand)sendStrategy.DoAlgorithm(falseid, new ActionCommand((object[]args) => {}));

        Assert.Throws<Exception>(() =>
        {
            c1.Execute();
        });

        var hardStopStrategy = new HardStopServerThreadCommandStrategy();

        var hs = (ICommand)hardStopStrategy.DoAlgorithm(id);

        hs.Execute();
    }
}
