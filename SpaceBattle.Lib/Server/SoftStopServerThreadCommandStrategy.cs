namespace SpaceBattle.Lib;
using Hwdtech;
using System.Collections.Concurrent;

public class SoftStopServerThreadCommandStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var id = (int)args[0];
        var action = () => {};
        if (args.Length == 2)
        {
            action = (Action)args[1];
        }

        ServerThread ?serverThread;

        var listThreads = IoC.Resolve<ConcurrentDictionary<int, ServerThread>>("ThreadMap");
        if(!listThreads.TryGetValue(id, out serverThread))
        {
            throw new Exception();
        }

        ISender ?sender;
        var listSender = IoC.Resolve<ConcurrentDictionary<int, ISender>>("SenderMap");
        if(!listSender.TryGetValue(id, out sender))
        {
            throw new Exception();
        }

        var cmd = new SoftStopServerThreadCommand(serverThread, sender, action);

        return new ChangeBehaviourCommand(serverThread, () => {
            serverThread.HandleCommand();
            cmd.Execute();
        });
    }
}
