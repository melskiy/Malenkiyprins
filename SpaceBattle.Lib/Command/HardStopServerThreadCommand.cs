namespace SpaceBattle.Lib;


public class HardStopServerThreadCommand: ICommand
{
    private ServerThread _serverThread;

    public HardStopServerThreadCommand(ServerThread serverThread)
    {
        _serverThread = serverThread;
    }

    public void Execute()
    {
        if(_serverThread != Thread.CurrentThread)
        {
            throw new Exception();
        }

        _serverThread.ServerThreadStop();
    }
}
