namespace SpaceBattle.Lib;
using Hwdtech;
using System.Collections.Concurrent;

public class SoftStopServerThreadCommand : ICommand
{
    private ServerThread _serverThread;

    private Action _action;
    public SoftStopServerThreadCommand(int id, Action action)
    {
        ServerThread? serverThread;
        if (!(IoC.Resolve<ConcurrentDictionary<int, ServerThread>>("ThreadMap").TryGetValue(id, out serverThread)))
        {
            throw new Exception();
        }
        _serverThread = serverThread;
        _action = action;
    }

    public void Execute()
    {

        if (_serverThread != Thread.CurrentThread)
        {
            throw new Exception();
        }
        var cmd = new ChangeBehaviourCommand(_serverThread, () =>
            {
                _serverThread.HandleCommand();
                if (_serverThread.isReceiverEmpty())
                {
                    _serverThread.ServerThreadStop();
                    _action();
                }
            });
            cmd.Execute();
    }
}
