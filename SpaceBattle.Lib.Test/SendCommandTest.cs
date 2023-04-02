using Hwdtech;
using Hwdtech.Ioc;
using System.Collections.Concurrent;

namespace SpaceBattle.Lib.Test;

public class SendCommandTest
{
    ConcurrentDictionary<string, ServerThread> threadMap = new ConcurrentDictionary<string, ServerThread>();
    ConcurrentDictionary<string, ISender> senderMap = new ConcurrentDictionary<string, ISender>();

    public SendCommandTest()
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
    public void UnsuccessfullSendCommandTestThrowsException()
    {
        var id = "1";
        var falseid = "3";

        IStrategy createAndStartSTStrategy = new CreateAndStartThreadStrategy();

        var c = (ICommand)createAndStartSTStrategy.DoAlgorithm(id);
        c.Execute();

        var sendStrategy = new SendCommandStrategy();

        Assert.Throws<Exception>(() =>
        {
            var c1 = (ICommand)sendStrategy.DoAlgorithm(falseid, new ActionCommand(() => {}));
        });

        var hardStopStrategy = new HardStopServerThreadCommandStrategy();

        var hs = (ICommand)hardStopStrategy.DoAlgorithm(id);

        hs.Execute();
    }
}
