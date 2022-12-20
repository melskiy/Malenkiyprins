using Hwdtech;
namespace SpaceBattle.Lib;
public class CreateMacroCommandStrategy : IStrategy
{

    public object DoAlgorithm(params object[] args)
    {
        string name = (string)args[0];
        IUObject uobj = (IUObject)args[1];
        IEnumerable<string> listcommands = IoC.Resolve<IEnumerable<string>>("SetUpOperation." + name);
        var listcommand = listcommands.Select(c => IoC.Resolve<ICommand>(c, uobj));
        ICommand macro = IoC.Resolve<ICommand>("Create.MacroCommand", listcommand);
        return macro;
    }
}

public class LongTermOperationStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        string name = (string)args[0];
        IUObject uobj = (IUObject)args[1];

        var macro = IoC.Resolve<ICommand>("CreateMacroCommandStrategy", name, uobj);

        ICommand repeatCommand = IoC.Resolve<ICommand>("Game.Command.Repeat", macro);
        ICommand inject_command = IoC.Resolve<ICommand>("Game.Command.Inject", repeatCommand);
        

        return inject_command;
    }
}
