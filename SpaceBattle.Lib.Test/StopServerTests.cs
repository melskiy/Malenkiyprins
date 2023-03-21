
using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;
using System.Collections.Concurrent;

public class StopServerTests

{
    [Fact]
    public void StopServerTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var mockSendCommandStrategy = new Mock<ICommand>();
        var mockThreadMap = new ConcurrentDictionary<int, ISender>();
        var send = new Mock<ISender>();

        mockThreadMap[1] = send.Object;
        mockThreadMap[2] = send.Object;
        mockThreadMap[3] = send.Object;


        var mockCommand = new Mock<SpaceBattle.Lib.ICommand>();
        int i = 0;
        mockCommand.Setup(x => x.Execute()).Callback(() => { i += 1; });
        var mockStrategyWithParams = new Mock<IStrategy>();
        mockStrategyWithParams.Setup(x => x.DoAlgorithm(It.IsAny<object[]>())).Returns(mockCommand.Object).Verifiable();


        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SenderMap", (object[] args) => mockThreadMap).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "StopServerStrategy", (object[] args) => new StopServerCommand()).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SendCommandStrategy", (object[] args) => mockStrategyWithParams.Object.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SoftStopServerThreadCommandStrategy", (object[] args) => mockStrategyWithParams.Object.DoAlgorithm(args)).Execute();


        IoC.Resolve<ICommand>("StopServerStrategy").Execute();

        Assert.Equal(3, i);
    }
}
