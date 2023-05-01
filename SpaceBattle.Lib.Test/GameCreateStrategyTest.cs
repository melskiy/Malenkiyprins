using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class GameCreateStrategyTest
{

    [Fact]
    public void SuccessfulGameCreateStrategyTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var mockCommand = new Mock<SpaceBattle.Lib.ICommand>();
        mockCommand.Setup(x => x.Execute());

        var mockStrategyWithParams = new Mock<IStrategy>();

        mockStrategyWithParams.Setup(x => x.DoAlgorithm(It.IsAny<object[]>())).Returns(mockCommand.Object);

        var mockStrategyReturnsDict = new Mock<IStrategy>();
        mockStrategyReturnsDict.Setup(x => x.DoAlgorithm()).Returns(new Dictionary<string, SpaceBattle.Lib.ICommand> { { "1", mockCommand.Object } });

        var mockStrategyReturnsDict2 = new Mock<IStrategy>();
        mockStrategyReturnsDict2.Setup(x => x.DoAlgorithm()).Returns(new Dictionary<string, object> { { "0", IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))) } });

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameScopeMap", (object[] args) => mockStrategyReturnsDict2.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameCommandMap", (object[] args) => mockStrategyReturnsDict.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Command.Macro", (object[] args) => mockStrategyWithParams.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Command.Inject", (object[] args) => mockStrategyWithParams.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Command.Repeat", (object[] args) => mockStrategyWithParams.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetCommand", (object[] args) => mockStrategyWithParams.Object.DoAlgorithm(args)).Execute();

        var Game = new GameCreateStrategy();
        var res = Game.DoAlgorithm("0");

        var map = IoC.Resolve<IDictionary<string, Lib.ICommand>>("GameCommandMap");
        Assert.Equal(map["0"], mockCommand.Object);

    }
}
