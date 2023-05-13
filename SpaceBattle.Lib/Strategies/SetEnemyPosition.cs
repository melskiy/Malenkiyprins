namespace SpaceBattle.Lib;
using Hwdtech;

public class SetEnemyPosition : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var Enum = (IEnumerator<object>)args[0];
        var ship = (string)args[1];
        return new SetEnemyPositionCommand(Enum, ship);
    }
}
