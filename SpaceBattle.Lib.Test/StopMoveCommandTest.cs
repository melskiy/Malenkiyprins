using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class StopMoveCommandTests
{
    public StopMoveCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        var mockCommand = new Mock<SpaceBattle.Lib.ICommand>();
        mockCommand.Setup(x => x.Execute());

        var mockInject = new Mock<IInjectable>();
        mockInject.Setup(x => x.Inject(It.IsAny<SpaceBattle.Lib.ICommand>()));

        var mockStrategyReturnCommand = new Mock<IStrategy>();
        mockStrategyReturnCommand.Setup(x => x.DoAlgorithm(It.IsAny<object[]>())).Returns(mockCommand.Object);

        var mockStrategyReturnEmpty = new Mock<IStrategy>();
        mockStrategyReturnEmpty.Setup(x => x.DoAlgorithm()).Returns(mockCommand.Object);

        var mockStrategyReturnIInjectable = new Mock<IStrategy>();
        mockStrategyReturnIInjectable.Setup(x => x.DoAlgorithm(It.IsAny<object[]>())).Returns(mockInject.Object);

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Commands.RemoveProperty", (object[] args) => mockStrategyReturnCommand.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Commands.EmptyCommand", (object[] args) => mockStrategyReturnEmpty.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Commands.GetProperty", (object[] args) => mockStrategyReturnIInjectable.Object.DoAlgorithm(args)).Execute();
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
