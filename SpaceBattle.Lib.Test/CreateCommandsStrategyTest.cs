using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class CreateCommandStrategyTests
{
    [Fact]
    public void SuccessfulCreateShootCommandStrategy()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var obj = new Mock<IUObject>();
        var shootable = new Mock<IShootable>();

        var adapterStrategy = new Mock<IStrategy>();
        adapterStrategy.Setup(s => s.DoAlgorithm(It.IsAny<object[]>())).Returns(shootable.Object);

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Adapter", (object[] args) => adapterStrategy.Object.DoAlgorithm(args)).Execute();

        var createShootCommandStrategy = new CreateShootCommandStrategy();
        
        var cmd = createShootCommandStrategy.DoAlgorithm(obj.Object);

        Assert.NotNull(cmd);
        Assert.True(cmd.GetType() == typeof(ShootCommand));
    }

    [Fact]
    public void SuccessfulCreateRotateCommandStrategy()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var obj = new Mock<IUObject>();
        var rotatable = new Mock<IRotatable>();

        var adapterStrategy = new Mock<IStrategy>();
        adapterStrategy.Setup(s => s.DoAlgorithm(It.IsAny<object[]>())).Returns(rotatable.Object);

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Adapter", (object[] args) => adapterStrategy.Object.DoAlgorithm(args)).Execute();

        var createRotateCommandStrategy = new CreateRotateCommandStrategy();
        
        var cmd = createRotateCommandStrategy.DoAlgorithm(obj.Object);

        Assert.NotNull(cmd);
        Assert.True(cmd.GetType() == typeof(RotateCommand));
    }

    [Fact]
    public void SuccessfulCreateStartMoveCommandStrategy()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var obj = new Mock<IUObject>();
        var moveStartable = new Mock<IMoveStartable>();

        var adapterStrategy = new Mock<IStrategy>();
        adapterStrategy.Setup(s => s.DoAlgorithm(It.IsAny<object[]>())).Returns(moveStartable.Object);

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Adapter", (object[] args) => adapterStrategy.Object.DoAlgorithm(args)).Execute();

        var createStartMoveCommandStrategy = new CreateStartMoveCommandStrategy();
        
        var cmd = createStartMoveCommandStrategy.DoAlgorithm(obj.Object);

        Assert.NotNull(cmd);
        Assert.True(cmd.GetType() == typeof(StartMoveCommand));
    }

    [Fact]
    public void SuccessfulCreateStoptMoveCommandStrategy()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var obj = new Mock<IUObject>();
        var moveStopable = new Mock<IMoveStopable>();

        var adapterStrategy = new Mock<IStrategy>();
        adapterStrategy.Setup(s => s.DoAlgorithm(It.IsAny<object[]>())).Returns(moveStopable.Object);

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Adapter", (object[] args) => adapterStrategy.Object.DoAlgorithm(args)).Execute();

        var createStopMoveCommandStrategy = new CreateStopMoveCommandStrategy();
        
        var cmd = createStopMoveCommandStrategy.DoAlgorithm(obj.Object);

        Assert.NotNull(cmd);
        Assert.True(cmd.GetType() == typeof(StopMoveCommand));
    }
}
