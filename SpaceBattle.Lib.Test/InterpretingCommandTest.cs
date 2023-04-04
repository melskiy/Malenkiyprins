using Hwdtech;
using Hwdtech.Ioc;

namespace SpaceBattle.Lib.Test;

public class InterpretingCommandTest
{
    [Fact]
    public void SuccessfulInterpretingCommand()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var newCmd = new Mock<ICommand>();
        newCmd.Setup(c => c.Execute());

        var createCommandStrategy = new Mock<IStrategy>();
        createCommandStrategy.Setup(s => s.DoAlgorithm(It.IsAny<IMessage>())).Returns(newCmd.Object).Verifiable();

        var queue = new Queue<ICommand>();

        var pushCommand = new Mock<ICommand>();
        pushCommand.Setup(c => c.Execute()).Callback(() => {
            queue.Enqueue(newCmd.Object);
        });

        var queuePushStrategy = new Mock<IStrategy>();
        queuePushStrategy.Setup(s => s.DoAlgorithm(It.IsAny<string>(), It.IsAny<ICommand>())).Returns(pushCommand.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.CreateCommand", (object[] args) => createCommandStrategy.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Queue.Push", (object[] args) => queuePushStrategy.Object.DoAlgorithm(args)).Execute();

        var message = new Mock<IMessage>();
        message.Setup(m => m.GameID).Returns("1").Verifiable();

        var interpretingCommand = new InterpretingCommand(message.Object);

        interpretingCommand.Execute();

        createCommandStrategy.Verify();
        message.VerifyAll();
        queuePushStrategy.VerifyAll();
        Assert.True(queue.Count == 1);
    }

    [Fact]
    public void UnsuccessfulInterpretingCommandThrowException()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var newCmd = new Mock<ICommand>();
        newCmd.Setup(c => c.Execute());

        var createCommandStrategy = new Mock<IStrategy>();
        createCommandStrategy.Setup(s => s.DoAlgorithm(It.IsAny<IMessage>())).Returns(newCmd.Object).Verifiable();

        var queue = new Queue<ICommand>();

        var pushCommand = new Mock<ICommand>();
        pushCommand.Setup(c => c.Execute()).Callback(() => {
            queue.Enqueue(newCmd.Object);
        });

        var queuePushStrategy = new Mock<IStrategy>();
        queuePushStrategy.Setup(s => s.DoAlgorithm(It.IsAny<int>(), It.IsAny<ICommand>())).Returns(pushCommand.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.CreateCommand", (object[] args) => createCommandStrategy.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Queue.Push", (object[] args) => queuePushStrategy.Object.DoAlgorithm(args)).Execute();

        var message = new Mock<IMessage>();
        message.Setup(m => m.GameID).Throws<Exception>();

        var interpretingCommand = new InterpretingCommand(message.Object);

        Assert.Throws<Exception>(() => interpretingCommand.Execute());
    }
}
