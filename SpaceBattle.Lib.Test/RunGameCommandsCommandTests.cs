using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class ReturnCommandTimeCommandTest
{
    [Fact]
    public void SuccessfulReturnCommandTimeCommand()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        var scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", scope).Execute();
        
        var queue = new Queue<ICommand>();

        queue.Enqueue(new ActionCommand(() => {
            IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", scope)).Execute();
            
            var strategy = new Mock<IStrategy>();
            strategy.Setup(s => s.DoAlgorithm()).Returns(0);

            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetQuantum", (object[] args) => strategy.Object.DoAlgorithm(args)).Execute();  
        }));

        queue.Enqueue(new ActionCommand(() => {}));
        queue.Enqueue(new ActionCommand(() => {}));

        var getQuantumStrategy = new Mock<IStrategy>();
        getQuantumStrategy.Setup(s => s.DoAlgorithm()).Returns(300);

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetCommandFromQueueStrategy", (object[] args) => queue.Dequeue()).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetQuantum", (object[] args) => getQuantumStrategy.Object.DoAlgorithm(args)).Execute();

        var returnCommandTimeCommand = new ReturnCommandTimeCommand(queue);

        Assert.Equal(300, IoC.Resolve<int>("GetQuantum"));

        returnCommandTimeCommand.Execute();

        Assert.Equal(0, IoC.Resolve<int>("GetQuantum"));
        Assert.True(queue.Count == 2);
    }

    [Fact]
    public void UnsuccessfulReturnCommandTimeCommand()
    {
        var handler = new Mock<ICommand>();
        handler.Setup(h => h.Execute()).Callback(() => {}).Verifiable();

        new InitScopeBasedIoCImplementationCommand().Execute();
        var scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", scope).Execute();
        
        var queue = new Queue<ICommand>();

        var cmd = new Mock<ICommand>();
        cmd.Setup(c => c.Execute()).Throws<Exception>();

        queue.Enqueue(cmd.Object);
        queue.Enqueue(new ActionCommand(() => {
            IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", scope)).Execute();
            
            var strategy = new Mock<IStrategy>();
            strategy.Setup(s => s.DoAlgorithm()).Returns(0);

            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetQuantum", (object[] args) => strategy.Object.DoAlgorithm(args)).Execute();  
        }));

        var getQuantumStrategy = new Mock<IStrategy>();
        getQuantumStrategy.Setup(s => s.DoAlgorithm()).Returns(300);

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetCommandFromQueueStrategy", (object[] args) => queue.Dequeue()).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "FindHandlerStrategy", (object[] args) => handler.Object).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetQuantum", (object[] args) => getQuantumStrategy.Object.DoAlgorithm(args)).Execute();

        var returnCommandTimeCommand = new ReturnCommandTimeCommand(queue);

        returnCommandTimeCommand.Execute();

        handler.VerifyAll();
    }
}
