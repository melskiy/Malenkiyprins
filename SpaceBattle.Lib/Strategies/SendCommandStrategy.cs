namespace SpaceBattle.Lib;
using Hwdtech;

public class SendCommandStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var id = (string)args[0];
        var message = (ICommand)args[1];
        var sender = IoC.Resolve<ISender>("GetSenderFromSenderMap", id);
        
        return new SendCommand(sender, message);
    }
}
