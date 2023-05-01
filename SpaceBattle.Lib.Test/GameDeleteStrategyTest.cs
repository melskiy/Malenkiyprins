using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class GameDeleteStrategyTest
{
 
    [Fact]
    public void SuccessfulGameDeleteStrategyTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var mockCommand = new Mock<SpaceBattle.Lib.ICommand>();
        mockCommand.Setup(x => x.Execute());

        var mockInject = new Mock<Iinjectable>();
        mockInject.Setup(x => x.Inject(It.IsAny<SpaceBattle.Lib.ICommand>())).Verifiable();

        var mockStrategyReturnIInjectable = new Mock<IStrategy>();
        mockStrategyReturnIInjectable.Setup(x => x.DoAlgorithm(It.IsAny<object[]>())).Returns(mockInject.Object);

        var mockStrategyReturnsDict = new Mock<IStrategy>();
        mockStrategyReturnsDict.Setup(x => x.DoAlgorithm()).Returns(new Dictionary<string, SpaceBattle.Lib.Iinjectable > { { "0", mockInject.Object} });

        var mockStrategyReturnsDict2 = new Mock<IStrategy>();
        mockStrategyReturnsDict2.Setup(x => x.DoAlgorithm()).Returns(new Dictionary<string, object > { { "0",  IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root")))} });

        var mockStrategyReturnEmpty = new Mock<IStrategy>();
        mockStrategyReturnEmpty.Setup(x => x.DoAlgorithm()).Returns(mockCommand.Object);

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameScopeMap", (object[] args) =>  mockStrategyReturnsDict2.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameCommandMap", (object[] args) => mockStrategyReturnsDict.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Commands.EmptyCommand", (object[] args) => mockStrategyReturnEmpty.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameDeleteStrategy", (object[] args) =>  new GameDeleteStrategy().DoAlgorithm(args)).Execute();
        
        IoC.Resolve<ICommand>("GameDeleteStrategy","0").Execute();
       
        var map = IoC.Resolve<IDictionary<string, object>>("GameScopeMap");
        Assert.Empty(map);
        
        mockInject.VerifyAll();
    }
}
