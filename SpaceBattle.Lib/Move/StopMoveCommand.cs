namespace SpaceBattle.Lib;
public class StopMoveCommand : ICommand
{
    IMoveStopable stopable;

    public StopMoveCommand(IMoveStopable stopable) 
    {
        this.stopable = stopable;
    }

    public void Execute()
    {
        IoC.Resolve<ICommand>(
            "Game.Commands.SetCommand",
            stopable.Target,
            "Movement",
            IoC.Resolve<ICommand>("Game.Command.Empty")
        ).Execute();
    }
}