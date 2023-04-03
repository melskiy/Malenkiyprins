namespace SpaceBattle.Lib;
using Hwdtech;

public class HttpCommandStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        return new HttpCommand((IMessage)args[0]);
    }
}
