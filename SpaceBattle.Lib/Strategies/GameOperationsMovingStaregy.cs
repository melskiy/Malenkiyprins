namespace SpaceBattle.Lib;
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
