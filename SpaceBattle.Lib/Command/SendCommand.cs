using Hwdtech;
namespace SpaceBattle.Lib;
using System.Collections.Concurrent;

public class SendCommand : ICommand
{
    private string _id;
    private ICommand _message;

    public SendCommand(string id, ICommand message)
    {
        _id = id;
        _message = message;
    }

    public void Execute()
    {
        ISender? sender;

        if (!(IoC.Resolve<ConcurrentDictionary<string, ISender>>("SenderMap").TryGetValue(_id, out sender)))
        {
            throw new Exception();
        }

        sender.Send(_message);
    }
}
