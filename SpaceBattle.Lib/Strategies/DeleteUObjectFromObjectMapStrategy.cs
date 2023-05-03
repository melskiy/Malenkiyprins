using Hwdtech;
namespace SpaceBattle.Lib;

public class DeleteUObjectFromObjectMapStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var id = (string)args[0];
        return new DeleteUObjectFromObjectMapCommand(id);
    }
}
