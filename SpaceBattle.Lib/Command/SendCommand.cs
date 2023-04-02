using Hwdtech;
namespace SpaceBattle.Lib;
using System.Collections.Concurrent;

public class SendCommand : ICommand
{
    private ISender _sender;
    private ICommand _message;

    public SendCommand(ISender sender, ICommand message)
    {
        _sender = sender;
        _message = message;
    }

    public void Execute()
    {
        _sender.Send(_message);
    }
}
