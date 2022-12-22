using Hwdtech;
namespace SpaceBattle.Lib;

public class RegisterHandlerCommand : ICommand
{
    private object _command, _exeption;
    private IHandler _handler;
    public RegisterHandlerCommand(object Command, object Exception, IHandler Handler)
    {
        this._command = Command;
        this._exeption = Exception;
        this._handler = Handler;
    }
    public void Execute()
    {
        var HandlerTree = IoC.Resolve<IDictionary<object, IDictionary<object, IHandler>>>("ExeptionTree");
        HandlerTree.TryAdd(_command, new Dictionary<object, IHandler>());
        HandlerTree[_command].TryAdd(_exeption, _handler);
    }
}
