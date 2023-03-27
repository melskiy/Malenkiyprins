namespace SpaceBattle.Lib;
using Hwdtech;

public class SoftStopServerThreadCommandStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var id = (string)args[0];
        var action = () => { };
        if (args.Length == 2)
        {
            action = (Action)args[1];
        }

        var cmd = new SoftStopServerThreadCommand(IoC.Resolve<ServerThread>("GetThreadFromThreadMap", id), action);

        return new SendCommand(IoC.Resolve<ISender>("GetSenderFromSenderMap", id), new ActionCommand(() =>
        {
            cmd.Execute();
        }));
    }
}
