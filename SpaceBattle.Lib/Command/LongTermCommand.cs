using Hwdtech;
namespace SpaceBattle.Lib;

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
