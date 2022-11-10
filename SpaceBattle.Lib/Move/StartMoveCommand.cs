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
    }


}


// IoC.Resolve<ICommand>("Queue.Push", q, cmd).Execute();