namespace SpaceBattle.Lib;

public class CreateAndStartThreadStrategy: IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var id = (int)args[0];
        var action = () => {};
        if (args.Length == 2)
        {
            action = (Action)args[1];
        }

        return new ActionCommand(() => {new CreateAndStartThreadCommand(id).Execute(); action();});
    }
}
