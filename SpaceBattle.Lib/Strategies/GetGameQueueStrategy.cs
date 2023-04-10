namespace SpaceBattle.Lib;
using Hwdtech;

public class GetGameQueueStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var id = (string)args[0];

        if (!(IoC.Resolve<IDictionary<string, Queue<ICommand>>>("GameQueueMap").TryGetValue(id, out Queue<ICommand>? queue)))
        {
            throw new Exception();
        }

        return queue;
    }
}
