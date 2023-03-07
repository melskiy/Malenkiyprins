namespace SpaceBattle.Lib;
using Hwdtech;
using System.Collections.Concurrent;

public class CreateAndStartThreadStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var id = (int)args[0];
        var action = () => {};
        if (args.Length == 2)
        {
            action = (Action)args[1];
        }

        var q = new BlockingCollection<ICommand>();
        var t = new ServerThread(new ReceiverAdapter(q));
        var s = new SenderAdapter(q);

        var listQueues = IoC.Resolve<ConcurrentDictionary<int, ISender>>("SenderMap");
        listQueues.TryAdd(id, s);

        var listThreads = IoC.Resolve<ConcurrentDictionary<int, ServerThread>>("ThreadMap");
        listThreads.TryAdd(id, t);

        return new ActionCommand(() => {t.ServerThreadStart(); action();});
    }
}
