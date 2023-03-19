namespace SpaceBattle.Lib;
using Hwdtech;
using System.Collections.Concurrent;

public class HardStopServerThreadCommand: ICommand
{
    private int id;
    private ServerThread _serverThread;

    public HardStopServerThreadCommand(int id)
    {
        ServerThread? serverThread;
        if (!(IoC.Resolve<ConcurrentDictionary<int, ServerThread>>("ThreadMap").TryGetValue(id, out serverThread)))
        {
            throw new Exception();
        }

        _serverThread = serverThread;
    }

    public void Execute()
    {
        // if(_serverThread != Thread.CurrentThread)
        // {
        //     throw new Exception();
        // }

        _serverThread.ServerThreadStop();
    }
}
