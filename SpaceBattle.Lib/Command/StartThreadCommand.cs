namespace SpaceBattle.Lib;
using Hwdtech;
using System.Collections.Concurrent;

public class StartThreadCommand : ICommand
{
    private string _id;

    public StartThreadCommand(string id)
    {
        _id = id;
    }

    public void Execute()
    {
        var listThreads = IoC.Resolve<ConcurrentDictionary<string, ServerThread>>("ThreadMap");
        
        if (!listThreads.TryGetValue(_id, out ServerThread? t))
        {
            throw new Exception();
        }

        t.ServerThreadStart();
    }
}
