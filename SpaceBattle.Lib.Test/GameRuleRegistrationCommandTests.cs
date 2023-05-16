using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class GameRuleRegistrationCommandTests
{
    [Fact]
    public void SuccessfulGameRuleRegistrationCommandExecute()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var queue = new Queue<ICommand>();

        var cmd = new Mock<ICommand>();
        cmd.Setup(c => c.Execute()).Callback(() => {});

        var getStrategy = new Mock<IStrategy>();
        getStrategy.Setup(s => s.DoAlgorithm()).Returns(cmd.Object).Verifiable();

        var pushCommand = new Mock<ICommand>();
        pushCommand.Setup(c => c.Execute()).Callback(() => queue.Enqueue(cmd.Object)).Verifiable();

        var pushStrategy = new Mock<IStrategy>();
        pushStrategy.Setup(s => s.DoAlgorithm(It.IsAny<string>(), It.IsAny<ICommand>())).Returns(pushCommand.Object).Verifiable();
        
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "RuleInicializationGameCommand", (object[] args) => getStrategy.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Queue.Push", (object[] args) => pushStrategy.Object.DoAlgorithm(args)).Execute();

        var id = "id";

        var gameRuleRegistrationCommand = new RegistrationGameRulesCommand(id);

        gameRuleRegistrationCommand.Execute();

        getStrategy.Verify();
        pushCommand.VerifyAll();
        pushStrategy.VerifyAll();
    }
}
