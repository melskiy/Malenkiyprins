using Hwdtech;
namespace SpaceBattle.Lib;

public class CreateStartMoveCommandStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var obj = (IUObject)args[0];
        return new StartMoveCommand(IoC.Resolve<IMoveStartable>("Adapter", typeof(IMoveStartable), obj));
    }
}
