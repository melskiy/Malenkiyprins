using Hwdtech;
using Hwdtech.Ioc;


namespace SpaceBattle.Lib.Test;

public class GetGameQueueStartegyTest
{
    Dictionary<string, Queue<ICommand>> queueMap = new Dictionary<string, Queue<ICommand>>(){
        {"1", new Queue<ICommand>()}
    };

    public GetGameQueueStartegyTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameQueueMap", (object[] args) => this.queueMap).Execute();
    }

    [Fact]
    public void SuccessfulGetGameQueueStartegy()
    {
        var id = "1";

        var strategy = new GetGameQueueStrategy();

        var q = strategy.DoAlgorithm(id);

        Assert.Equal(this.queueMap[id], q);
    }

    [Fact]
    public void UnsuccessfulGetGameQueueStartegy()
    {
        var falseID = "2";

        var strategy = new GetGameQueueStrategy();

        Assert.Throws<Exception>(() => strategy.DoAlgorithm(falseID));
    }
}
