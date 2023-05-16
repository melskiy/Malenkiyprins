using Hwdtech;
namespace SpaceBattle.Lib;

public class CreateOperationStrategy: IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var obj = (IUObject)args[0];
        var type = (string)args[1];

        var rulesList = IoC.Resolve<IEnumerable<string>>("Game.Rules.Get." + type);
        var commandList = rulesList.ToList().Select(rule => IoC.Resolve<ICommand>("Game.Command.Create", rule, obj));

        return IoC.Resolve<ICommand>("Game.Command.Macro.Create", commandList);
    }
}
