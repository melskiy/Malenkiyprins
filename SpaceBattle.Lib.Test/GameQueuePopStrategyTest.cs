using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class GameQueuePopStrategyTest
{
    [Fact]
    public void SuccessfulGameQueuePopStrategyTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var queue = new Queue<ICommand>();
        var cmd = new Mock<ICommand>();

        var getGameQueueByIDStrategy = new Mock<IStrategy>();
        getGameQueueByIDStrategy.Setup(s => s.DoAlgorithm(It.IsAny<string>())).Returns(queue);

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetGameQueue", (object[] args) => getGameQueueByIDStrategy.Object.DoAlgorithm(args)).Execute();
        var gameQueuePushStrategy = new GameQueuePushStrategy();

        var push = (Lib.ICommand)gameQueuePushStrategy.DoAlgorithm("1", cmd.Object);

        push.Execute();

        Assert.NotNull(IoC.Resolve<Queue<ICommand>>("GetGameQueue", "1"));

        var gameQueuePopStrategy = new GameQueuePopStrategy();

        var pop = gameQueuePopStrategy.DoAlgorithm("1",queue);
        Assert.Empty(IoC.Resolve<Queue<ICommand>>("GetGameQueue", "1"));
        cmd.Equals(pop);
    }

}
