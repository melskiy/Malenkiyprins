using Hwdtech;
using System.Diagnostics;
namespace SpaceBattle.Lib;

public class ReturnCommandTimeCommand : ICommand
{
    Queue<ICommand> _gameQueue; 

    public ReturnCommandTimeCommand(Queue<ICommand> gameQueue)
    {
        _gameQueue = gameQueue;
    }

    public void Execute()
    {
        var stopwatch = new Stopwatch();
        var time = 0;
        stopwatch.Start();
        while(time < IoC.Resolve<int>("GetQuantum"))
        {
            var cmd = IoC.Resolve<ICommand>("GetCommandFromQueueStrategy", _gameQueue);
            try
            {
                cmd.Execute();
            }
            catch(Exception excepton)
            {
                IoC.Resolve<ICommand>("FindHandlerStrategy", excepton, cmd).Execute();
            }

            time = (int)stopwatch.Elapsed.TotalMilliseconds;
        }

        stopwatch.Stop();
    }  
}
