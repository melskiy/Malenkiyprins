using Hwdtech;
namespace SpaceBattle.Lib;

public class InterpretingCommand: ICommand
{
    IMessage _message;

    public InterpretingCommand(IMessage message)
    {
        _message = message;
    }

    public void Execute()
    {
        var cmd = IoC.Resolve<ICommand>("Game.CreateCommand", _message);

        var id = _message.GameID;

        IoC.Resolve<ICommand>("Game.Queue.Push", id, cmd).Execute();
    }
}
