namespace SpaceBattle.Lib;
using Hwdtech;

public class GameQueuePushCommand: ICommand
{
    string _id;
    ICommand _cmd;

    public GameQueuePushCommand(string id, ICommand cmd)
    {
        _id = id;
        _cmd = cmd;
    }

    public void Execute()
    {
        var queue = IoC.Resolve<Queue<ICommand>>("GetGameQueue", _id);
        queue.Enqueue(_cmd);
    }
}
