using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class CodeStringGenerateStrategyTest
{
    [Fact]
    public void SuccessfulCodeStringGenerateStrategy()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        var scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));

        var str = "rendered_template";

        var adapterBuilder = new Mock<IBuilder>();
        adapterBuilder.Setup(c => c.Build()).Returns(str).Verifiable();
        adapterBuilder.Setup(c => c.AddProperty(It.IsAny<object>())).Callback(() => {}).Verifiable();

        var getAdapterBuilderStrategy = new Mock<IStrategy>();
        getAdapterBuilderStrategy.Setup(s => s.DoAlgorithm(It.IsAny<object[]>())).Returns(adapterBuilder.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetAdapterBuilder", (object[] args) => getAdapterBuilderStrategy.Object.DoAlgorithm(args)).Execute();

        var codeStringGenerateStrategy = new CodeStringGenerateStrategy();
        var codeString = (string)codeStringGenerateStrategy.DoAlgorithm(typeof(IUObject), typeof(IMovable));

        Assert.True(codeString == str);
        adapterBuilder.VerifyAll();
        getAdapterBuilderStrategy.VerifyAll();
    }
}
