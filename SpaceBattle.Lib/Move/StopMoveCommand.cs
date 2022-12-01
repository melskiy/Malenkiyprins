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
        IoC.Resolve<ICommand>("Game.Commands.RemoveProperty", stopable.Target, "Speed").Execute();
        IoC.Resolve<Iinjectable>("Game.Commands.GetProperty", stopable.Target, "Moving").Inject(IoC.Resolve<ICommand>("Game.Commands.EmptyCommand"));
        
    }
}