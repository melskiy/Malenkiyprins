using Hwdtech;
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


public class GameOperationsMovingStaregy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {

        IUObject obj = (IUObject)args[0];

        IEnumerable<ICommand> list_command = new List<ICommand>();

        IoC.Resolve<IEnumerable<string>>("Game.Config.ForMove").ToList().ForEach(str => list_command.Append(IoC.Resolve<ICommand>(str, obj)));

        ICommand macroCommand = IoC.Resolve<ICommand>("Game.Command.Macro", list_command);

        ICommand inject_command = IoC.Resolve<ICommand>("Game.Command.Inject", macroCommand);

        ICommand repeatCommand = IoC.Resolve<ICommand>("Game.Command.Repeat", inject_command);
        list_command.Append(repeatCommand);

        return inject_command;

    }
}
