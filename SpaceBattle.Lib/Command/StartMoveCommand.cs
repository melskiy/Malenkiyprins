namespace SpaceBattle.Lib;

public class StartMoveCommand : ICommand
{
    private IMoveStartable startable;
    public StartMoveCommand(IMoveStartable startable)
    {
        this.startable = startable;
    }

    public void Execute()
    {
        startable.Properties.ToList().ForEach(o => IoC.Resolve<ICommand>("Game.Commands.SetProperty", startable.Target, o.Key, o.Value).Execute());

        ICommand cmd = IoC.Resolve<ICommand>("Game.Operations.Moving", startable.Target);
        IoC.Resolve<ICommand>("Game.Commands.SetProperty", startable.Target, "move", cmd);
        IoC.Resolve<ICommand>("Game.Queue.Push", IoC.Resolve<Queue<ICommand>>("Game.Queue"), cmd).Execute();

    }

}
