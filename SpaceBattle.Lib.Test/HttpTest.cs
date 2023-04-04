using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class HttpTest
{
    [Fact]
    public void CreateHttpTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var mockCommand = new Mock<SpaceBattle.Lib.ICommand>();
        mockCommand.Setup(x => x.Execute()).Verifiable();

        Mock<SpaceBattle.Lib.IMessage> messege = new Mock<SpaceBattle.Lib.IMessage>();
        messege.SetupGet(x => x.GameID).Returns("1").Verifiable();

        var mockStrategyWithParams = new Mock<IStrategy>();
        mockStrategyWithParams.Setup(x => x.DoAlgorithm(It.IsAny<object[]>())).Returns(mockCommand.Object).Verifiable();

        var mockStrategyReturnsDict = new Mock<IStrategy>();
        mockStrategyReturnsDict.Setup(x => x.DoAlgorithm()).Returns(new Dictionary<string, int> { { "1", 0 } }).Verifiable();

        var gtyti = new HandleCommandStrategy();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "HandleCommandStrategy", (object[] args) => gtyti.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game-Thread", (object[] args) => mockStrategyReturnsDict.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SendCommandStrategy", (object[] args) => mockStrategyWithParams.Object.DoAlgorithm(args)).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameCreateCommandStrategy", (object[] args) => mockStrategyWithParams.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<ICommand>("HandleCommandStrategy", messege.Object).Execute();

        messege.VerifyAll();
        mockCommand.VerifyAll();
    }
}
