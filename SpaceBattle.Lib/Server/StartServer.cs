namespace SpaceBattle.Lib;

public class StartServerStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        return new StartServer((int)args[0]);
    }
}
