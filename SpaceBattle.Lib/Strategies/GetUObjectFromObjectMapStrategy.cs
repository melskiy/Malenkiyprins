using Hwdtech;
namespace SpaceBattle.Lib;

public class GetUObjectFromObjectMapStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var id = (string)args[0];

        if (!IoC.Resolve<IDictionary<string, IUObject>>("GetUObjects").TryGetValue(id, out IUObject? obj))
        {
            throw new Exception();
        }

        return obj; 
    }
}
