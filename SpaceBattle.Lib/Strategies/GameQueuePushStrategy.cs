namespace SpaceBattle.Lib;

public class GameQueuePushStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var id = (string)args[0];
        var cmd = (ICommand)args[1];

        return new GameQueuePushCommand(id, cmd);
    }
}
