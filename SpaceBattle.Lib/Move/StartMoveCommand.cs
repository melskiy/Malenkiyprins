namespace SpaceBattle.Lib;
public class StartMoveCommand : ICommand
{
    IMoveStartable startable;

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
