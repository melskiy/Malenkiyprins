namespace SpaceBattle.Lib;
public class StartMoveCommand : ICommand
{
    IMoveStartable startable;
    public StartMoveCommand(IMoveStartable startable)
    {
        this.startable = startable;
    }

    public void Execute()
    {
        IoC.Resolve<ICommand>(
            "Game.Commands.SetProperty",
            startable.Target,
            "Velocity",
            startable.InitialVelocity
        ).Execute();

        ICommand cmd  = IoC.Resolve<ICommand>("Game.Commands.Move", startable.Target);
        IoC.Resolve<ICommand>("Game.Queue.Push", IoC.Resolve<IQueue<ICommand>>("Game.Queue"), cmd).Execute();
        

    }

}
