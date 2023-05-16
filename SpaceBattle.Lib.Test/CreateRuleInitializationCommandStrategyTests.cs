using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;


public class RegisteringCreatingOperationsStrategyTest
{
    [Fact]
    public void SuccessfulRegisteringCreatingOperationStrategyRunStrategy()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var ruleList = new List<string>(){
            "BurnFuel"
        };

        var ruleListStrategy = new Mock<IStrategy>();
        ruleListStrategy.Setup(s => s.DoAlgorithm()).Returns(ruleList);
        
        var burnFuelCommand = new Mock<ICommand>();
        burnFuelCommand.Setup(c => c.Execute()).Callback(() => {}).Verifiable();

        var burnFuelCommandStrategy = new Mock<IStrategy>();
        burnFuelCommandStrategy.Setup(s => s.DoAlgorithm(It.IsAny<IUObject>())).Returns(burnFuelCommand.Object);

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Rules.Get.Rotate", (object[] args) => ruleListStrategy.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Command.BurnFuel", (object[] args) => burnFuelCommandStrategy.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Command.Macro.Create", (object[] args) => burnFuelCommand.Object).Execute();

        var ruleInicializationStrategy = new RuleInicializationGameCommand();
        var actionCommand = (ICommand)ruleInicializationStrategy.DoAlgorithm();

        Assert.IsType<ActionCommand>(actionCommand);

        actionCommand.Execute();

        var obj = new Mock<IUObject>();

        var result = IoC.Resolve<ICommand>("Game.Operations.Create", obj.Object, "Rotate");

        result.Execute();

        burnFuelCommand.VerifyAll();
    }
}
