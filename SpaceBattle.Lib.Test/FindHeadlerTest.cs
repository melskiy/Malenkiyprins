using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class FindHandlerTests
{
    Mock<ICommand> Handler = new Mock<ICommand>();
    public FindHandlerTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        var StrategyReturnsDict = new Dictionary<int, ICommand>();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "ExceptionTree", (object[] args) => StrategyReturnsDict).Execute();
        var ghcs = new GetHashStrategy();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetHashStrategy", (object[] args) => ghcs.DoAlgorithm(args)).Execute();

        var fndhs = new FindHandlerStrategy();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "FindHandlerStrategy", (object[] args) => fndhs.DoAlgorithm(args)).Execute();
    }
    [Fact]
    public void PositiveFindHandler()
    {
        var cmd = new RegisterHandlerCommand(new List<Type> { typeof(MoveCommand), typeof(ArgumentException) }, Handler.Object);
        cmd.Execute();
        var fnd = IoC.Resolve<ICommand>("FindHandlerStrategy", new List<Type> { typeof(MoveCommand), typeof(ArgumentException) });
        Assert.Equal(fnd, Handler.Object);
    }

    [Fact]
    public void NegativeFindHandler()
    {
        var tree = IoC.Resolve<IDictionary<int, ICommand>>("ExceptionTree");
        tree[0] = Handler.Object;
        var fnd = IoC.Resolve<ICommand>("FindHandlerStrategy", new List<Type> { typeof(MoveCommand), typeof(MockException) });
        Assert.Equal(fnd, Handler.Object);
    }
}
