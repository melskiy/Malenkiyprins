using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class GameCommandTests
{
    [Fact]
    public void SuccessfulGameCommandExecute()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        var scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", scope).Execute();

        var queue = new Queue<ICommand>();

        var cmd = new Mock<ICommand>();
        cmd.Setup(c => c.Execute()).Callback(() => {}).Verifiable();

        var returnCommandTimeStrategy = new Mock<IStrategy>();
        returnCommandTimeStrategy.Setup(s => s.DoAlgorithm(It.IsAny<Queue<ICommand>>())).Returns(cmd.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "ReturnCommandTimeStrategy", (object[] args) => returnCommandTimeStrategy.Object.DoAlgorithm(args)).Execute();

        var game = new GameAsCommand(queue, scope);

        var newScope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", newScope).Execute();

        game.Execute();

        Assert.True(scope == IoC.Resolve<object>("Scopes.Current"));
        cmd.VerifyAll();
        returnCommandTimeStrategy.VerifyAll();
    }
}
