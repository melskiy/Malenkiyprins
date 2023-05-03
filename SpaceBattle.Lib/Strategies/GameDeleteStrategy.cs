using Hwdtech;
namespace SpaceBattle.Lib;

public class GameDeleteStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        return new GameDeleteCommand((string)args[0]);
    }
}
