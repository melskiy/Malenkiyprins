namespace SpaceBattle.Lib;

public class CreateAndStartThreadStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var id = (string)args[0];
        var action = () => {};

        if (args.Length == 2)
        {
            action = (Action)args[1];
        }

        return new ActionCommand((object[] args) =>
        {
            new CreateThreadCommand(id).Execute();
            new StartThreadCommand(id).Execute();
            action();
        });
    }
}
