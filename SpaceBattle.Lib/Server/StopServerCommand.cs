namespace SpaceBattle.Lib;
using Hwdtech;
using System.Collections.Concurrent;

public class StopServerCommand : ICommand
{
    public void Execute()
    {
        try{
            IoC.Resolve<ConcurrentDictionary<int,ServerThread>>("ThreadMap").ToList().ForEach(item => IoC.Resolve<ICommand>("SoftStopThread",item.Key));
        }catch (Exception e){
            IoC.Resolve<ICommand>("FindHandlerStrategy",typeof(SoftStopServerThreadCommand),e.GetType());
        }
       
    }
}