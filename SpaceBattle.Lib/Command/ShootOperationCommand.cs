using Hwdtech;
namespace SpaceBattle.Lib;

public class ShootOperationCommand : ICommand
{
    private IShootable _shootable;
    public ShootOperationCommand(IShootable shootable)
    {
        _shootable = shootable;
    }

    public void Execute()
    {
        ICommand cmd = IoC.Resolve<ICommand>("Game.Operations.Shooting", _shootable);
        IoC.Resolve<ICommand>("Game.Queue.Push", IoC.Resolve<Queue<ICommand>>("Game.Queue"), cmd).Execute();
    }
}
