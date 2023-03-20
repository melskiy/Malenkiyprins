
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
        
        
        var sndcmdstra = new SendCommandStrategy();
        var sftstps = new SoftStopServerThreadCommandStrategy();
        
        var strtservstra = new StartServerStrategy();
        var crandstrtthread = new CreateAndStartThreadStrategy();


        ConcurrentDictionary<int, ServerThread> mapServerThreads = new ConcurrentDictionary<int, ServerThread>();
        ConcurrentDictionary<int, ISender> mapServerThreadsSenders = new ConcurrentDictionary<int, ISender>();
        
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "ThreadMap", (object[] args) => mapServerThreads).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SenderMap", (object[] args) => mapServerThreadsSenders).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "StartServerStrategy",  (object[] args) =>  strtservstra.DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CreateAndStartThreadStrategy",  (object[] args) => crandstrtthread.DoAlgorithm(args)).Execute();
        const int length = 24;

        IoC.Resolve<ICommand>("StartServerStrategy",length).Execute();





        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "StopServerStrategy",  (object[] args) =>  new StopServerCommand()).Execute();
   
    


        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SendCommandStrategy", (object[] args) => sndcmdstra).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SoftStopServerThreadCommandStrategy", (object[] args) => sftstps).Execute();
        
        IoC.Resolve<ICommand>("StopServerStrategy").Execute();


    }
}