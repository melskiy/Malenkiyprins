
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
        var crandstrtthread = new CreateAndStartThreadStrategy();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "StartServerStrategy",  (object[] args) =>  strtservstra.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CreateAndStartThreadStrategy",  (object[] args) => crandstrtthread.DoAlgorithm(args)).Execute();
       
        ConcurrentDictionary<int, ServerThread> mapServerThreads = new ConcurrentDictionary<int, ServerThread>();
        ConcurrentDictionary<int, ISender> mapServerThreadsSenders = new ConcurrentDictionary<int, ISender>();


        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "ThreadMap", (object[] args) => mapServerThreads).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SenderMap", (object[] args) => mapServerThreadsSenders).Execute();
    
        IoC.Resolve< SpaceBattle.Lib.ICommand>("StartServerStrategy",length).Execute();
    }
}