using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class ShootOperationCommandTest
{
    [Fact]
    public void SuccessfulShootOperationExecute()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var queue = new Queue<ICommand>();

        var cmd = new Mock<ICommand>();
        cmd.Setup(c => c.Execute());

        var queuePushCommand = new Mock<ICommand>();
        queuePushCommand.Setup(c => c.Execute()).Callback(() => queue.Enqueue(cmd.Object)).Verifiable();

        var queuePushStrategy = new Mock<IStrategy>();
        queuePushStrategy.Setup(s => s.DoAlgorithm(It.IsAny<Queue<ICommand>>(), It.IsAny<ICommand>())).Returns(queuePushCommand.Object).Verifiable();

        var getQueueStrategy = new Mock<IStrategy>();
        getQueueStrategy.Setup(s => s.DoAlgorithm()).Returns(queue).Verifiable();

        var shootStrategy = new Mock<IStrategy>();
        shootStrategy.Setup(s => s.DoAlgorithm(It.IsAny<object[]>())).Returns(cmd.Object).Verifiable();
        
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Queue", (object[] args) => getQueueStrategy.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Operations.Shooting", (object[] args) => shootStrategy.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Queue.Push", (object[] args) => queuePushStrategy.Object.DoAlgorithm(args)).Execute();

        var obj = new Mock<IShootable>();
        var shootOperationCommand = new ShootOperationCommand(obj.Object);

        shootOperationCommand.Execute();

        Assert.NotEmpty(queue);

        queuePushCommand.VerifyAll();
        getQueueStrategy.VerifyAll();
        shootStrategy.Verify();
        queuePushStrategy.VerifyAll();
    }
}
