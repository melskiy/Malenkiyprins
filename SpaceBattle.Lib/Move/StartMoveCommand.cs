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

        IMovable movable = IoC.Resolve<IMovable>("GenerateAdapter", typeof(MovableAdapter), startable.Target);
        
    }

}


// IoC.Resolve<ICommand>("Queue.Push", q, cmd).Execute();