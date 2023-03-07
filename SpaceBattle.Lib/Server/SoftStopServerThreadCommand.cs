namespace SpaceBattle.Lib;

public class SoftStopServerThreadCommand: ICommand
{
    private ServerThread _serverThread;
    private ISender _sender;
    private Action _action;

    public SoftStopServerThreadCommand(ServerThread serverThread, ISender sender, Action action)
    {
        _serverThread = serverThread;
        _sender = sender;
        _action = action;
    }

    public void Execute()
    {
        if(_serverThread == Thread.CurrentThread )
        {
            if(_sender.isEmpty())
            {
                _serverThread.ServerThreadStop();
                _action();
            }
        }
    }
}
