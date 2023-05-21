using Hwdtech;
namespace SpaceBattle.Lib;

public class CreateRotateCommandStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var obj = (IUObject)args[0];
        return new RotateCommand(IoC.Resolve<IRotatable>("Adapter", typeof(IRotatable), obj));
    }
}
