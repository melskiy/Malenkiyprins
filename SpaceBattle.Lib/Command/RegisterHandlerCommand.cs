using Hwdtech;
namespace SpaceBattle.Lib;


public class RegisterHandlerCommand : ICommand
{
    private IEnumerable<Type> _list_of_types;
    private ICommand _handler;
    public RegisterHandlerCommand(IEnumerable<Type> list_of_types, ICommand handler)
    {
        _list_of_types = list_of_types;
        _handler = handler;
    }
    public void Execute()
    {
        var handlerTree = IoC.Resolve<IDictionary<int, ICommand>>("ExceptionTree");
        var hashes = IoC.Resolve<int>("GetHashStrategy", _list_of_types);
        handlerTree.TryAdd(hashes, _handler);
    }
}
