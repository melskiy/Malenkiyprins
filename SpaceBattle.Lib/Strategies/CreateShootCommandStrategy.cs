using Hwdtech;
namespace SpaceBattle.Lib;

public class CreateShootCommandStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var obj = (IUObject)args[0];
        return new ShootCommand(IoC.Resolve<IShootable>("Adapter", typeof(IShootable), obj));
    }
}
