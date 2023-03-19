namespace SpaceBattle.Lib;
using Hwdtech;
using System.Collections.Concurrent;

public class HardStopServerThreadCommandStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var id = (int)args[0];
        var action = () => {};
        if (args.Length == 2)
        {
            action = (Action)args[1];
        }

        var cmd = new HardStopServerThreadCommand(id);

        return new SendCommand(id, new ActionCommand(() => {
            cmd.Execute(); action();
        }));
    }
}