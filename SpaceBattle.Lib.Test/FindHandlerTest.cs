using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class FindHeadlerTests
{    
    Mock<IHandler> Handler = new Mock<IHandler>();
    public FindHeadlerTests(){
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        var mockStrategyReturnsDict = new Mock<IStrategy>();
        mockStrategyReturnsDict.Setup(x => x.DoAlgorithm()).Returns(new Dictionary<object, IDictionary<object,IHandler>>{{typeof(MoveCommand), new Dictionary<object,IHandler>(){{typeof(ArgumentException), Handler.Object}}}});

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "ExeptionTree", (object[] args) => mockStrategyReturnsDict.Object.DoAlgorithm(args)).Execute();
        
    }
    [Fact]
    public void Treshka()
    {
        var TreeFind = new FindHandler().DoAlgorithm(typeof(MoveCommand),typeof(ArgumentException));
        Assert.Equal(TreeFind.GetType(),Handler.Object.GetType());

    }

    [Fact]
    public void Treshka2()
    {

        var Tree = IoC.Resolve<IDictionary <object,IDictionary<object,IHandler>>>("ExeptionTree");
        var Tree2 = Tree[typeof(MoveCommand)];
        Tree2.Add("default",Handler.Object);
        var TreeFind2 = new FindHandler().DoAlgorithm(typeof(MoveCommand),typeof(MockException));
        Assert.Equal(TreeFind2.GetType(),Handler.Object.GetType());
        
    }
}
