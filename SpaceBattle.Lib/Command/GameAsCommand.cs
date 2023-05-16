using Hwdtech;
namespace SpaceBattle.Lib;

public class GameAsCommand : ICommand
{
    private Queue<ICommand> _gameQueue;
    private object _gameScope;
    public GameAsCommand(Queue<ICommand> gameQueue, object gameScope)
    {
        _gameQueue = gameQueue;
        _gameScope = gameScope;
    }
    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", _gameScope).Execute();
        IoC.Resolve<ICommand>("ReturnCommandTimeStrategy", _gameQueue).Execute();
    }
}
