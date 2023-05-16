namespace SpaceBattle.Lib;

public class GetCommandFromQueueStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var gameQueue = (Queue<ICommand>)args[0];
        if (!gameQueue.TryDequeue(out ICommand? cmd)) 
            throw new Exception();
            
        return cmd;
    }
}
