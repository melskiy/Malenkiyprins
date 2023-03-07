namespace SpaceBattle.Lib;
using Hwdtech;

public class SendCommandStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var id = (int)args[0];
        var message = (ICommand)args[1];
        return new SendCommand(id, message);
    }
}
