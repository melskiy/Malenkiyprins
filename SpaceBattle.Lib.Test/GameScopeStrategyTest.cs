using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class GameScopeStrategyTest
{
 
    [Fact]
    public void SuccessfulGameScopeStrategyTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var mockStrategyReturnsDict2 = new Mock<IStrategy>();
        mockStrategyReturnsDict2.Setup(x => x.DoAlgorithm()).Returns(new Dictionary<string, object >());


        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameScopeMap", (object[] args) =>  mockStrategyReturnsDict2.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameScopeStrategy", (object[] args) =>  new GameScopeStrategy().DoAlgorithm(args)).Execute();
        
        var map = IoC.Resolve<IDictionary<string, object>>("GameScopeMap");
        IoC.Resolve<ICommand>("GameScopeStrategy","0",IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root")),4.0).Execute();

        Assert.Single(map);
        Assert.NotEqual(map["0"],IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root")));
    }
}
