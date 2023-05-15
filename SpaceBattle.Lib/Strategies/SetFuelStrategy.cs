namespace SpaceBattle.Lib;
using Hwdtech;

public class SetFuelStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var Enum = (IEnumerator<object>)args[0];
        var ship = (string)args[1];
        return new SetFuelCommad(Enum, ship);
    }
}
