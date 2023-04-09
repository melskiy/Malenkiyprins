namespace SpaceBattle.Lib;
using Hwdtech;

public class GameQueuePushStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var id = (string)args[0];
        var cmd = (ICommand)args[1];
        var queue = IoC.Resolve<Queue<ICommand>>("GetGameQueue", id);

        return new GameQueuePushCommand(queue, cmd);
    }
}
