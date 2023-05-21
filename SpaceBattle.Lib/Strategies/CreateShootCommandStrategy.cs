using Hwdtech;
namespace SpaceBattle.Lib;

public class CreateShootOperationCommandStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var obj = (IUObject)args[0];
        return new ShootOperationCommand(IoC.Resolve<IShootable>("Adapter", typeof(IShootable), obj));
    }
}
