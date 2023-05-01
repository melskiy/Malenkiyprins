namespace SpaceBattle.Lib;

public class GameQueuePopCommand : ICommand
{

    Queue<ICommand> _queue;

    public GameQueuePopCommand(Queue<ICommand> queue)
    {
        _queue = queue;
    }

    public void Execute()
    {
        _queue.Dequeue();
    }
}
