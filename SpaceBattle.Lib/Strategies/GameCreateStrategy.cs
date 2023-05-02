using Hwdtech;
namespace SpaceBattle.Lib;

public class GameCreateStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var gameid = (string)args[0];
        var queue = new Queue<ICommand>();
        var scope = IoC.Resolve<IDictionary<string, object>>("GameScopeMap")[gameid];

        var game = IoC.Resolve<ICommand>("GetCommand", queue, scope);
        var GameCommandMap = IoC.Resolve<IDictionary<string, ICommand>>("GameCommandMap");

        IEnumerable<ICommand> list_command = new List<ICommand>();
        ICommand macroCommand = IoC.Resolve<ICommand>("Game.Command.Macro", list_command);
        ICommand inject_command = IoC.Resolve<ICommand>("Game.Command.Inject", macroCommand);
        ICommand repeatCommand = IoC.Resolve<ICommand>("Game.Command.Repeat", inject_command);
        list_command.Append(repeatCommand);

        GameCommandMap.Add(gameid, inject_command);

        return inject_command;
    }
}
