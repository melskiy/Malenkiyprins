using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class RegisterHandlerCommandTest
{
    Mock<IHandler> Handler = new Mock<IHandler>();
    public RegisterHandlerCommandTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        var mockStrategyReturnsDict = new Mock<IStrategy>();
        mockStrategyReturnsDict.Setup(x => x.DoAlgorithm()).Returns(new Dictionary<object, IDictionary<object, IHandler>>());

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "ExeptionTree", (object[] args) => mockStrategyReturnsDict.Object.DoAlgorithm(args)).Execute();

    }
    
    [Fact]
    public void Treshka()
    {
        var cmd = new RegisterHandlerCommand(typeof(MoveCommand), typeof(ArgumentException), Handler.Object);
        cmd.Execute();
        var Tree = IoC.Resolve<IDictionary<object, IDictionary<object, IHandler>>>("ExeptionTree");
        var Tree2 = Tree[typeof(MoveCommand)];
        var Tree3 = Tree2[typeof(ArgumentException)];
        Assert.Equal(Tree3, Handler.Object);
    }
}
