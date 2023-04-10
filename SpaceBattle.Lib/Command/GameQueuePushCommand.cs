namespace SpaceBattle.Lib;

public class GameQueuePushCommand: ICommand
{
    ICommand _cmd;

    Queue<ICommand> _queue;

    public GameQueuePushCommand(Queue<ICommand> queue, ICommand cmd)
    {
        _queue = queue;
        _cmd = cmd;
    }

    public void Execute()
    {
        _queue.Enqueue(_cmd);
    }
}
