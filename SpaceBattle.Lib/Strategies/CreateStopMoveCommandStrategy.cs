using Hwdtech;
namespace SpaceBattle.Lib;

public class CreateStopMoveCommandStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var obj = (IUObject)args[0];
        return new StopMoveCommand(IoC.Resolve<IMoveStopable>("Adapter", typeof(IMoveStopable), obj));
    }
}
