using Hwdtech;
using Hwdtech.Ioc;
using System.Collections.Concurrent;

namespace SpaceBattle.Lib.Test;

public class StartTreadCommandTest
{
    ConcurrentDictionary<int, ServerThread> threadMap = new ConcurrentDictionary<int, ServerThread>();
    ConcurrentDictionary<int, ISender> senderMap = new ConcurrentDictionary<int, ISender>();

    public StartTreadCommandTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "ThreadMap", (object[] args) => threadMap).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SenderMap", (object[] args) => senderMap).Execute();
    }

    [Fact]
    public void UnsuccessfulStartTreadCommandThrowExeption()
    {
        var id = 9;
        var falseid = 10;

        var cv = new AutoResetEvent(false);

        ICommand createTCommand = new CreateThreadCommand(id);
        createTCommand.Execute();

        Assert.Throws<Exception>(() =>
        {
            new StartThreadCommand(falseid).Execute();
        });
    }
}
