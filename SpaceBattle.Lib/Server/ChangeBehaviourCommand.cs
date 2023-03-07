namespace SpaceBattle.Lib;

public class ChangeBehaviourCommand : ICommand
{
    private ServerThread _serverThread;
    private Action _strategy;

    public ChangeBehaviourCommand(ServerThread serverThread, Action strategy)
    {
        _serverThread = serverThread;
        _strategy = strategy;
    }

    public void Execute()
    {
        _serverThread.ChangeBehaviour(_strategy);
    }
}