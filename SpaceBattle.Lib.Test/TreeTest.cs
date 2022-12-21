using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class TreeTest
{    
    public TreeTest(){
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var mockStrategyReturnsDict = new Mock<IStrategy>();
        mockStrategyReturnsDict.Setup(x => x.DoAlgorithm()).Returns(new Dictionary<int, object>());

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "ICollisionTreeRootDictionary", (object[] args) => mockStrategyReturnsDict.Object.DoAlgorithm(args)).Execute();
    }
    [Fact]
    public void TreeTests()
    {

        ICommand Tree = new TreeCreate("../../../Vectors.txt");

  
        Tree.Execute();
        var Tree2 = IoC.Resolve<IDictionary<int, object>>("ICollisionTreeRootDictionary");

        Assert.True(Tree2.ContainsKey(12));
        Assert.True(Tree2.ContainsKey(434));
        Assert.True(Tree2.ContainsKey(2));

        Assert.True(((IDictionary<int, object>) Tree2[12]).ContainsKey(32));
        Assert.True(((IDictionary<int, object>) Tree2[434]).ContainsKey(2));

        Assert.True(((IDictionary<int, object>)((IDictionary<int, object>) Tree2[12])[32]).ContainsKey(56));
    }
}
