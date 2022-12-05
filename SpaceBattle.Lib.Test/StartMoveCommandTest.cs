using Moq;

namespace SpaceBattle.Lib.Test;

public class StartMoveCommandTests
{
    public StartMoveCommandTests()
    {
        var mockCommand = new Mock<ICommand>();
        mockCommand.Setup(x => x.Execute());

        var mockStrategyWithParams = new Mock<IStrategy>();
        mockStrategyWithParams.Setup(x => x.DoAlgorithm(It.IsAny<object[]>())).Returns(mockCommand.Object);

        var mockStrategyWithoutParams = new Mock<IStrategy>();
        mockStrategyWithoutParams.Setup(x => x.DoAlgorithm()).Returns(new Queue<ICommand>());
        

        var mockStrategyReturnString = new Mock<IStrategy>();
        mockStrategyReturnString.Setup(x => x.DoAlgorithm()).Returns(new List<string>());
        
        IoC.Resolve<ICommand>("IoC.Add", "Game.Commands.SetProperty", mockStrategyWithParams.Object).Execute();
        IoC.Resolve<ICommand>("IoC.Add", "Game.Operations.Moving", mockStrategyWithParams.Object).Execute();
        IoC.Resolve<ICommand>("IoC.Add", "Game.Queue.Push", mockStrategyWithParams.Object).Execute();
        IoC.Resolve<ICommand>("IoC.Add", "Game.Queue", mockStrategyWithoutParams.Object).Execute();
        IoC.Resolve<ICommand>("IoC.Add", "Game.Config.ForMove", mockStrategyReturnString.Object).Execute();
        IoC.Resolve<ICommand>("IoC.Add", "Game.Command.Macro", mockStrategyWithParams.Object).Execute();
        IoC.Resolve<ICommand>("IoC.Add", "Game.Command.Inject", mockStrategyWithParams.Object).Execute();
        IoC.Resolve<ICommand>("IoC.Add", "Game.Command.Repeat", mockStrategyWithParams.Object).Execute();
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
