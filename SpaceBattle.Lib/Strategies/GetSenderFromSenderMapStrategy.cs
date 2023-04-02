namespace SpaceBattle.Lib;
using Hwdtech;
using System.Collections.Concurrent;

public class GetSenderFromSenderMapStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var id = (string)args[0];

        if (!(IoC.Resolve<ConcurrentDictionary<string, ISender>>("SenderMap").TryGetValue(id, out ISender? sender)))
        {
            throw new Exception();
        }

        return sender;
    }
}
