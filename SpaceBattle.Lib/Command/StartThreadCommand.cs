namespace SpaceBattle.Lib;
using Hwdtech;
using System.Collections.Concurrent;

public class StartThreadCommand : ICommand
{
    private int _id;

    public StartThreadCommand(int id)
    {
        _id = id;
    }

    public void Execute()
    {
        var listThreads = IoC.Resolve<ConcurrentDictionary<int, ServerThread>>("ThreadMap");
        
        if (!listThreads.TryGetValue(_id, out ServerThread? t))
        {
            throw new Exception();
        }

        t.ServerThreadStart();
    }
}
