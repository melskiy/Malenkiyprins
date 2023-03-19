namespace SpaceBattle.Lib;
using System.Collections.Concurrent;

public class SenderAdapter : ISender
{
    private BlockingCollection<ICommand>_queue;

    public SenderAdapter(BlockingCollection<ICommand> queue)
    {
        _queue = queue;
    }

    public void Send(ICommand message)
    {
        _queue.Add(message);
    }
}
