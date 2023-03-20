namespace SpaceBattle.Lib;
using System.Collections.Concurrent;

public class ReceiverAdapter : IReceiver
{
    private BlockingCollection<ICommand>_queue;

    public ReceiverAdapter(BlockingCollection<ICommand> queue)
    {
        _queue = queue;
    }

    public bool isEmpty()
    {
        return _queue.Count() == 0;
    }

    public ICommand Receive()
    {
        return _queue.Take();
    }
}
