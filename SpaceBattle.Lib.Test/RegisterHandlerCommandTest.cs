using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class RegisterHandlerCommandTest
{
    Mock<ICommand> Handler = new Mock<ICommand>();
    public RegisterHandlerCommandTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var mockStrategyReturnsDict = new Mock<IStrategy>();
        mockStrategyReturnsDict.Setup(x => x.DoAlgorithm()).Returns(new Dictionary<int, ICommand>());
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "ExceptionTree", (object[] args) => mockStrategyReturnsDict.Object.DoAlgorithm(args)).Execute();

        var ghcs = new GetHashStrategy();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetHashStrategy", (object[] args) => ghcs.DoAlgorithm(args)).Execute();

    }
    [Fact]
    public void RegisterHandleTest()
    {
        var cmd = new RegisterHandlerCommand(new List<Type> { typeof(MoveCommand), typeof(ArgumentException) }, Handler.Object);
        cmd.Execute();
        var Tree = IoC.Resolve<IDictionary<int, ICommand>>("ExceptionTree");
        IEnumerable<Type> Types = new List<Type> { typeof(MoveCommand), typeof(ArgumentException) };

        var Hashs = IoC.Resolve<int>("GetHashStrategy", Types.OrderBy(x => x.GetHashCode()));
        Assert.Equal(Tree[Hashs], Handler.Object);
    }
}
