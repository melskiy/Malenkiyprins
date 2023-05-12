namespace SpaceBattle.Lib;

public class ReturnCommandTimeStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var gameQueue = (Queue<ICommand>)args[0]; 
        return new ReturnCommandTimeCommand(gameQueue);
    }
}
