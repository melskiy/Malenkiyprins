using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class StartMoveCommandTests
{
    public StartMoveCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        
        var mockCommand = new Mock<SpaceBattle.Lib.ICommand>();
        mockCommand.Setup(x => x.Execute());

        var mockStrategyWithParams = new Mock<IStrategy>();
        mockStrategyWithParams.Setup(x => x.DoAlgorithm(It.IsAny<object[]>())).Returns(mockCommand.Object);

        var mockStrategyWithoutParams = new Mock<IStrategy>();
        mockStrategyWithoutParams.Setup(x => x.DoAlgorithm()).Returns(new Queue<SpaceBattle.Lib.ICommand>());
        
        var mockStrategyReturnString = new Mock<IStrategy>();
        mockStrategyReturnString.Setup(x => x.DoAlgorithm()).Returns(new List<string>());
        
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Commands.SetProperty", (object[] args) => mockStrategyWithParams.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Operations.Moving", (object[] args) => mockStrategyWithParams.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Queue.Push", (object[] args) =>  mockStrategyWithParams.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Queue", (object[] args) => mockStrategyWithoutParams.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Config.ForMove", (object[] args) => mockStrategyReturnString.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Command.Macro", (object[] args) => mockStrategyWithParams.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Command.Inject", (object[] args) => mockStrategyWithParams.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Command.Repeat", (object[] args) => mockStrategyWithParams.Object.DoAlgorithm(args)).Execute();
    }                                           

    [Fact]
    public void PositiveTest()
    {                                                                                                                             
        var startable = new Mock<IMoveStartable>();
        var obj = new Mock<IUObject>();

        startable.SetupGet(a => a.Target).Returns(obj.Object).Verifiable();
        startable.SetupGet(a => a.Properties).Returns(new Dictionary<string, object>(){{"Velocity", new Vector(1, 1)}}).Verifiable();

        ICommand startMove = new StartMoveCommand(startable.Object);

        startMove.Execute();

        startable.VerifyAll();
    }

    [Fact]
    public void TargetMethodReturnsException()
    {
        var startable = new Mock<IMoveStartable>();

        startable.SetupGet(a => a.Target).Throws<Exception>().Verifiable();
        startable.SetupGet(a => a.Properties).Returns(new Dictionary<string, object>(){{"Velocity", new Vector(1, 1)}}).Verifiable();

        ICommand startMove = new StartMoveCommand(startable.Object);

        Assert.Throws<Exception>(() => startMove.Execute());

    }

    [Fact]
    public void VelocityMethodReturnsException()
    {
        var startable = new Mock<IMoveStartable>();
        var obj = new Mock<IUObject>();

        startable.SetupGet(a => a.Target).Returns(obj.Object).Verifiable();
        startable.SetupGet(a => a.Properties).Throws<Exception>().Verifiable();

        ICommand startMove = new StartMoveCommand(startable.Object);

        Assert.Throws<Exception>(() => startMove.Execute());
    }

    [Fact]
    public void OperationMoveStrategyTest()
    {
        var startable = new Mock<IMoveStartable>();
        var obj = new Mock<IUObject>();

        startable.SetupGet(a => a.Target).Returns(obj.Object);
        startable.SetupGet(a => a.Properties).Throws<Exception>();

        IStrategy GameOperationsMovingStaregy = new GameOperationsMovingStaregy();

        Assert.NotNull(GameOperationsMovingStaregy.DoAlgorithm(startable.Object.Target));
    }
}
