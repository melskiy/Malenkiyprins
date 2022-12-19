using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class CollisionTest
{    
    public void CollisionTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        var obj1 = new Mock<IUObject>();
        var obj2 = new Mock<IUObject>();

        var mockCommand = new Mock<SpaceBattle.Lib.ICommand>();
        mockCommand.Setup(x => x.Execute());
        var mockStrategyReturnsCommand = new Mock<IStrategy>();
        mockStrategyReturnsCommand.Setup(x => x.DoAlgorithm(obj1.Object,obj2.Object)).Returns(mockCommand.Object).Verifiable();

        var mockStrategyReturnsDict = new Mock<IStrategy>();
        var mockStrategyReturnsList = new Mock<IStrategy>();
        mockStrategyReturnsDict.Setup(x => x.DoAlgorithm()).Returns(new Dictionary<int, object>()).Verifiable();
        mockStrategyReturnsList.Setup(x => x.DoAlgorithm(It.IsAny<object[]>())).Returns(new List<int>()).Verifiable();

        var mockStrategyReturnsCorrentList = new Mock<IStrategy>();
        mockStrategyReturnsCorrentList.Setup(x => x.DoAlgorithm(It.IsAny<object[]>())).Returns(new List<int>(){12,32,56,4}).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "ICollisionTreeRootDictionary", (object[] args) => mockStrategyReturnsDict.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Commands.GetCollicionPropertys", (object[] args) => mockStrategyReturnsList.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Colision", (object[] args) => mockStrategyReturnsCommand.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "PrepareData", (object[] args) => mockStrategyReturnsCorrentList.Object.DoAlgorithm(args)).Execute();




        ICommand Tree = new TreeCreate("../../../Vectors.txt");
        Tree.Execute();

        ICommand IsCollision = new IsCollision(obj1.Object, obj2.Object);

        IsCollision.Execute();
          mockStrategyReturnsCorrentList.VerifyAll();
          mockStrategyReturnsCommand.VerifyAll();
          mockStrategyReturnsList.VerifyAll();
          mockStrategyReturnsDict.VerifyAll();
       
        
    }

    [Fact]
    public void PrepareDataTest()
    {
        List<int> property1 = new List<int>(){12,32,56,4};
        List<int> property2 = new List<int>(){12,32,56,4};
        IStrategy PrepareData = new PrepareData();
        Object.Equals(new List<int>(){0,0,0,0}, PrepareData.DoAlgorithm(property1,property2));
    }
}
