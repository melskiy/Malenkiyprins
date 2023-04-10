using Hwdtech;
using Hwdtech.Ioc;

namespace SpaceBattle.Lib.Test;

public class GameCreateCommandStrategyTest
{
    [Fact]
    public void SuccessfulGameCreateCommandStrategy()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var obj = new Mock<IUObject>();

        var getUObjectFromUObjectMapStrtegy = new Mock<IStrategy>();
        getUObjectFromUObjectMapStrtegy.Setup(s => s.DoAlgorithm(It.IsAny<string>())).Returns(obj.Object).Verifiable();

        var setPropsCommand = new Mock<ICommand>();
        setPropsCommand.Setup(c => c.Execute()).Callback(() => {});

        var setPropsCommandStrategy = new Mock<IStrategy>();
        setPropsCommandStrategy.Setup(s => s.DoAlgorithm(It.IsAny<object[]>())).Returns(setPropsCommand.Object).Verifiable();

        var cmd = new Mock<ICommand>();

        var createCommandStrategy = new Mock<IStrategy>();
        createCommandStrategy.Setup(s => s.DoAlgorithm(It.IsAny<IUObject>())).Returns(cmd.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUObjectFromObjectMap", (object[] args) => getUObjectFromUObjectMapStrtegy.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameUObjectSetProperty", (object[] args) => setPropsCommandStrategy.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CreateCommand.Test", (object[] args) => createCommandStrategy.Object.DoAlgorithm(args)).Execute();

        var message = new Mock<IMessage>();
        message.Setup(m => m.OrderType).Returns("Test").Verifiable();
        message.Setup(m => m.GameItemID).Returns("1").Verifiable();
        message.Setup(m => m.Properties).Returns(new Dictionary<string, object>(){
            {"dlfkldkf", 3}
        }).Verifiable();

        var strstegy = new GameCreateCommandStrategy();
        var c = strstegy.DoAlgorithm(message.Object);

        message.VerifyAll();
        createCommandStrategy.VerifyAll();
        setPropsCommandStrategy.VerifyAll();
        getUObjectFromUObjectMapStrtegy.VerifyAll();
        Assert.NotNull(c);
    }

    [Fact]
    public void UnuccessfulGameCreateCommandStrategyThrowTypeException()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var obj = new Mock<IUObject>();

        var getUObjectFromUObjectMapStrtegy = new Mock<IStrategy>();
        getUObjectFromUObjectMapStrtegy.Setup(s => s.DoAlgorithm(It.IsAny<string>())).Returns(obj.Object).Verifiable();

        var setPropsCommand = new Mock<ICommand>();
        setPropsCommand.Setup(c => c.Execute()).Callback(() => {});

        var setPropsCommandStrategy = new Mock<IStrategy>();
        setPropsCommandStrategy.Setup(s => s.DoAlgorithm(It.IsAny<object[]>())).Returns(setPropsCommand.Object).Verifiable();

        var cmd = new Mock<ICommand>();

        var createCommandStrategy = new Mock<IStrategy>();
        createCommandStrategy.Setup(s => s.DoAlgorithm(It.IsAny<IUObject>())).Returns(cmd.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUObjectFromObjectMap", (object[] args) => getUObjectFromUObjectMapStrtegy.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameUObjectSetProperty", (object[] args) => setPropsCommandStrategy.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CreateCommand.Test", (object[] args) => createCommandStrategy.Object.DoAlgorithm(args)).Execute();

        var message = new Mock<IMessage>();
        message.Setup(m => m.OrderType).Throws<Exception>();

        var strstegy = new GameCreateCommandStrategy();

        Assert.Throws<Exception>(() => strstegy.DoAlgorithm(message.Object));
    }

    [Fact]
    public void UnuccessfulGameCreateCommandStrategyRunStrategyThrowGameItemIDException()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var obj = new Mock<IUObject>();

        var getUObjectFromUObjectMapStrtegy = new Mock<IStrategy>();
        getUObjectFromUObjectMapStrtegy.Setup(s => s.DoAlgorithm(It.IsAny<int>())).Returns(obj.Object).Verifiable();

        var setPropsCommand = new Mock<ICommand>();
        setPropsCommand.Setup(c => c.Execute()).Callback(() => {});

        var setPropsCommandStrategy = new Mock<IStrategy>();
        setPropsCommandStrategy.Setup(s => s.DoAlgorithm(It.IsAny<object[]>())).Returns(setPropsCommand.Object).Verifiable();

        var cmd = new Mock<ICommand>();

        var createCommandStrategy = new Mock<IStrategy>();
        createCommandStrategy.Setup(s => s.DoAlgorithm(It.IsAny<IUObject>())).Returns(cmd.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUObjectFromObjectMap", (object[] args) => getUObjectFromUObjectMapStrtegy.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameUObjectSetProperty", (object[] args) => setPropsCommandStrategy.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CreateCommand.Test", (object[] args) => createCommandStrategy.Object.DoAlgorithm(args)).Execute();

        var message = new Mock<IMessage>();
        message.Setup(m => m.GameItemID).Throws<Exception>();

        var strstegy = new GameCreateCommandStrategy();

        Assert.Throws<Exception>(() => strstegy.DoAlgorithm(message.Object));
    }

    [Fact]
    public void UnuccessfulGameCreateCommandStrategyRunStrategyThrowPropertiesException()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var obj = new Mock<IUObject>();

        var getUObjectFromUObjectMapStrtegy = new Mock<IStrategy>();
        getUObjectFromUObjectMapStrtegy.Setup(s => s.DoAlgorithm(It.IsAny<string>())).Returns(obj.Object).Verifiable();

        var setPropsCommand = new Mock<ICommand>();
        setPropsCommand.Setup(c => c.Execute()).Callback(() => {});

        var setPropsCommandStrategy = new Mock<IStrategy>();
        setPropsCommandStrategy.Setup(s => s.DoAlgorithm(It.IsAny<object[]>())).Returns(setPropsCommand.Object).Verifiable();

        var cmd = new Mock<ICommand>();

        var createCommandStrategy = new Mock<IStrategy>();
        createCommandStrategy.Setup(s => s.DoAlgorithm(It.IsAny<IUObject>())).Returns(cmd.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUObjectFromObjectMap", (object[] args) => getUObjectFromUObjectMapStrtegy.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameUObjectSetProperty", (object[] args) => setPropsCommandStrategy.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CreateCommand.Test", (object[] args) => createCommandStrategy.Object.DoAlgorithm(args)).Execute();

        var message = new Mock<IMessage>();
        message.Setup(m => m.Properties).Throws<Exception>();

        var strstegy = new GameCreateCommandStrategy();

        Assert.Throws<Exception>(() => strstegy.DoAlgorithm(message.Object));
    }
}
