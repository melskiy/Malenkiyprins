namespace SpaceBattle.Lib;
using Hwdtech;
using System.Collections.Concurrent;

public class StopServerCommand : ICommand
{
    public void Execute()
    {
        IoC.Resolve<ConcurrentDictionary<int, ISender>>("SenderMap").ToList().ForEach(
            item => IoC.Resolve<ICommand>("SendCommandStrategy",
                item.Key, IoC.Resolve<ICommand>("SoftStopServerThreadCommandStrategy", item.Key)).Execute()
        );

    }
}