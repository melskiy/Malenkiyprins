namespace SpaceBattle.Lib;
using Hwdtech;
using System.Collections.Concurrent;

public class GetThreadFromThreadMapStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var id = (string)args[0];

        if (!(IoC.Resolve<ConcurrentDictionary<string, ServerThread>>("ThreadMap").TryGetValue(id, out ServerThread? serverThread)))
        {
            throw new Exception();
        }

        return serverThread;
    }
}
