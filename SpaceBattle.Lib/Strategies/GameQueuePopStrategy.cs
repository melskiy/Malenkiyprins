namespace SpaceBattle.Lib;
using Hwdtech;

public class GameQueuePopStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var id = (string)args[0];
        var queue = IoC.Resolve<Queue<ICommand>>("GetGameQueue", id);

        return new GameQueuePopCommand(queue);
    }
}
