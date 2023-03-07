using Hwdtech;
namespace SpaceBattle.Lib;
using System.Collections.Concurrent;

public class SendCommand : ICommand
{
    private int _id;
    private ICommand _message;

    public SendCommand(int id, ICommand message)
    {
        _id = id;
        _message = message;
    }

    void ICommand.Execute()
    {
        ISender ?sender;

        if(!(IoC.Resolve<ConcurrentDictionary<int, ISender>>("SenderMap").TryGetValue(_id, out sender)))
        {
            throw new Exception();
        }
        sender.Send(_message);
    }
}
