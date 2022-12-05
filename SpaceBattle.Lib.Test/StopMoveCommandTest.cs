using Moq;

namespace SpaceBattle.Lib.Test;

public class StopMoveCommandTests
{
    public StopMoveCommandTests()
    {
        var mockCommand = new Mock<ICommand>();
        mockCommand.Setup(x => x.Execute());

        var mockInject = new Mock<IInjectable>();
        mockInject.Setup(x => x.Inject(It.IsAny<ICommand>()));

        var mockStrategyReturnCommand = new Mock<IStrategy>();
        mockStrategyReturnCommand.Setup(x => x.DoAlgorithm(It.IsAny<object[]>())).Returns(mockCommand.Object);

        var mockStrategyReturnEmpty = new Mock<IStrategy>();
        mockStrategyReturnEmpty.Setup(x => x.DoAlgorithm()).Returns(mockCommand.Object);

        var mockStrategyReturnIInjectable = new Mock<IStrategy>();
        mockStrategyReturnIInjectable.Setup(x => x.DoAlgorithm(It.IsAny<object[]>())).Returns(mockInject.Object);

        IoC.Resolve<ICommand>("IoC.Add", "Game.Commands.RemoveProperty", mockStrategyReturnCommand.Object).Execute();
        IoC.Resolve<ICommand>("IoC.Add", "Game.Commands.EmptyCommand", mockStrategyReturnEmpty.Object).Execute();
        IoC.Resolve<ICommand>("IoC.Add", "Game.Commands.GetProperty", mockStrategyReturnIInjectable.Object).Execute();
}

    [Fact]
    public void PositiveTest()
    {
        var stopable = new Mock<IMoveStopable>();
        var obj = new Mock<IUObject>();

        stopable.SetupGet(a => a.Target).Returns(obj.Object).Verifiable();
        stopable.SetupGet(a => a.Properties).Returns(new List<string>(){"Velocity"}).Verifiable();

        ICommand stopMove = new StopMoveCommand(stopable.Object);

        stopMove.Execute();

        stopable.VerifyAll();
    }

    [Fact]
    public void TargetMethodReturnsException()
    {
        var stopable = new Mock<IMoveStopable>();

        stopable.SetupGet(a => a.Target).Throws<Exception>().Verifiable();
        stopable.SetupGet(a => a.Properties).Returns(new List<string>(){"Speed"}).Verifiable();

        ICommand stopMove = new StopMoveCommand(stopable.Object);

        Assert.Throws<Exception>(() => stopMove.Execute());

    }

    [Fact]
    public void VelocityMethodReturnsException()
    {
        var stopable = new Mock<IMoveStopable>();
        var obj = new Mock<IUObject>();

        stopable.SetupGet(a => a.Target).Returns(obj.Object).Verifiable();
        stopable.SetupGet(a => a.Properties).Throws(new Exception()).Verifiable();

        ICommand startMove = new StopMoveCommand(stopable.Object);

        Assert.Throws<Exception>(() => startMove.Execute());
    }
}
