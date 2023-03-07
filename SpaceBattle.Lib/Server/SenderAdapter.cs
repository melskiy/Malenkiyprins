namespace SpaceBattle.Lib;
using System.Collections.Concurrent;

public class SenderAdapter : ISender
{
    private BlockingCollection<ICommand>_queue;

    public SenderAdapter(BlockingCollection<ICommand> queue)
    {
        _queue = queue;
    }

    public bool isEmpty()
    {
        return _queue.Count() == 0;
    }

    public void Send(ICommand message)
    {
        _queue.Add(message);
    }
}
