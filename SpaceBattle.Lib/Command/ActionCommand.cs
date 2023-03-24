namespace SpaceBattle.Lib;

public class ActionCommand : ICommand
{
    private Action<object[]> _strategy;

    private object[] _args;

    public ActionCommand(Action<object[]> strategy, params object[] args)
    {
        _strategy = strategy;
        _args = args;
    }

    public void Execute()
    {
        _strategy(_args);
    }
}
