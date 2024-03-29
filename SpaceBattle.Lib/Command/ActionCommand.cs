namespace SpaceBattle.Lib;

public class ActionCommand : ICommand
{
    private Action _strategy;

    public ActionCommand(Action strategy)
    {
        _strategy = strategy;
    }

    public void Execute()
    {
        _strategy();
    }
}
