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
        stopable.Properties.ToList<string>().ForEach(x => IoC.Resolve<ICommand>("Game.Commands.RemoveProperty",stopable.Target,x).Execute());
        IoC.Resolve<IInjectable>("Game.Commands.GetProperty", stopable.Target, "Moving").Inject(IoC.Resolve<ICommand>("Game.Commands.EmptyCommand"));
    }
}
