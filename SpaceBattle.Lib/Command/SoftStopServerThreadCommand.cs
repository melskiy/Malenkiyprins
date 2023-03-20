namespace SpaceBattle.Lib;
using Hwdtech;
using System.Collections.Concurrent;

public class SoftStopServerThreadCommand : ICommand
{
    private ServerThread _serverThread;

    private Action _action;
    public SoftStopServerThreadCommand(ServerThread serverThread, Action action)
    {
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
                if (_serverThread.isReceiverEmpty())
                {
                    _serverThread.ServerThreadStop();
                    _action();
                }
                else
                {
                    _serverThread.HandleCommand();
                }
            });
            cmd.Execute();
    }
}
