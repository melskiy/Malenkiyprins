namespace SpaceBattle.Lib;
using Hwdtech;
using System.Collections.Concurrent;

public class CreateThreadCommand : ICommand
{
    private string _id;

    public CreateThreadCommand(string id)
    {
        _id = id;
    }

    public void Execute()
    {
        var q = new BlockingCollection<ICommand>();
        var t = new ServerThread(new ReceiverAdapter(q));
        var s = new SenderAdapter(q);

        var listQueues = IoC.Resolve<ConcurrentDictionary<string, ISender>>("SenderMap");
        listQueues.TryAdd(_id, s);

        var listThreads = IoC.Resolve<ConcurrentDictionary<string, ServerThread>>("ThreadMap");
        listThreads.TryAdd(_id, t);
    }
}
