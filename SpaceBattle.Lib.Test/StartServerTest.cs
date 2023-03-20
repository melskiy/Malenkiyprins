
using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;
using System.Collections.Concurrent;

public class StartServerTests

{
    [Fact]
    public void Execute_CreatesAndStartsThreads()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
       
        const int length = 24;
        var strtservstra = new StartServerStrategy();
        

        var mockCommand = new Mock<SpaceBattle.Lib.ICommand>();
        int i = 0;
        mockCommand.Setup(x => x.Execute()).Callback(() => {i+=1; });

        var mockStrategyWithParams = new Mock<IStrategy>();
        mockStrategyWithParams.Setup(x => x.DoAlgorithm(It.IsAny<object[]>())).Returns(mockCommand.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "StartServerStrategy",  (object[] args) =>  strtservstra.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CreateAndStartThreadStrategy",  (object[] args) =>  mockStrategyWithParams.Object.DoAlgorithm(args)).Execute();
       

        IoC.Resolve<ICommand>("StartServerStrategy",length).Execute();



        Assert.Equal(length,i);

        

    
    }
}