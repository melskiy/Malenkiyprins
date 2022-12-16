using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class MacroCommandTests
{
    public MacroCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var mockCommand = new Mock<SpaceBattle.Lib.ICommand>();
        mockCommand.Setup(x => x.Execute());

        var mockStrategyWithParams = new Mock<IStrategy>();
        mockStrategyWithParams.Setup(x => x.DoAlgorithm(It.IsAny<object[]>())).Returns(mockCommand.Object);

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CreateMacroCommandTestStrategy", (object[] args) => mockStrategyWithParams.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "LongTermOperationStrategy", (object[] args) => mockStrategyWithParams.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.SetUpOperations.Moving", (object[] args) => mockStrategyWithParams.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Create.MacroCommand", (object[] args) =>  mockStrategyWithParams.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Command.Inject", (object[] args) => mockStrategyWithParams.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Command.Repeat", (object[] args) => mockStrategyWithParams.Object.DoAlgorithm(args)).Execute();
    }

    [Fact]
    public void CreateMacroCommandTestStrategyTest()
    {
        IStrategy CreateMacroCommand = new CreateMacroCommandStrategy();
        string name = "dsadasdads";
        var obj = new Mock<IUObject>();
        Assert.NotNull(CreateMacroCommand.DoAlgorithm(name, obj.Object));
    }
    [Fact]
    public void LongTermOperationStrategyTest()
    {
        IStrategy LongTermOperation = new LongTermOperationStrategy();
        string name = "dsadasdads";
        var obj = new Mock<IUObject>();
        Assert.NotNull(LongTermOperation.DoAlgorithm(name, obj.Object));
    }
}
