using Hwdtech;
namespace SpaceBattle.Lib;

public class ServerThread
{
    private IReceiver _queue;
    private Thread _thread;
    private bool _isActive = true;
    private Action _strategy;
    public ServerThread(IReceiver queue)
    {
        _queue = queue;
        _strategy = () => HandleCommand();
        _thread = new Thread(() => {
            while(_isActive) _strategy();
        });
    }

    internal void ServerThreadStart()
    {
        _thread.Start();
    }

    internal void ServerThreadStop()
    {
        _isActive = false;
    }

    internal void HandleCommand()
    {
        var cmd = _queue.Receive();
        try
        {
            cmd.Execute();
        }
        catch(Exception exception)
        {
            IoC.Resolve<ICommand>("FindHandlerStrategy", new List<Type>{cmd.GetType(), exception.GetType()}).Execute();
        }
    }

    internal void ChangeBehaviour(Action strategy) => _strategy = strategy;

    public override bool Equals(object? obj) => obj is Thread thread && thread == _thread;

    public override int GetHashCode() => _thread.GetHashCode();

    public static bool operator ==(ServerThread t1, Thread t2) => t1._thread == t2;

    public static bool operator !=(ServerThread t1, Thread t2) => !(t1._thread == t2);

    internal bool isReceiverEmpty() => _queue.isEmpty();
}
