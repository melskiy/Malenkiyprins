namespace SpaceBattle.Lib;
using Hwdtech;

public class HandleCommandStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        return new HandleCommand((IMessage)args[0]);
    }
}
