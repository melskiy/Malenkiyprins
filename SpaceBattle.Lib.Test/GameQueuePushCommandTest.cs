using Hwdtech;
using Hwdtech.Ioc;

namespace SpaceBattle.Lib.Test;

public class GameQueuePushCommandTest
{
    [Fact]
    public void SuccessfulGameQueuePushCommand()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var queue = new Queue<ICommand>();

        var cmd = new Mock<ICommand>();

        var getGameQueueByIDStrategy = new Mock<IStrategy>();
        getGameQueueByIDStrategy.Setup(s => s.DoAlgorithm(It.IsAny<string>())).Returns(queue).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetGameQueue", (object[] args) => getGameQueueByIDStrategy.Object.DoAlgorithm(args)).Execute();

        var gameQueuePushCommand = new GameQueuePushCommand("1", cmd.Object);

        gameQueuePushCommand.Execute();

        Assert.True(queue.Count == 1);
        getGameQueueByIDStrategy.VerifyAll();
    }
}
