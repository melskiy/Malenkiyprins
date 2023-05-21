using Hwdtech;
namespace SpaceBattle.Lib;

public class RegistrationGameRulesCommand : ICommand
{
    private string _gameId;
    public RegistrationGameRulesCommand(string gameId)
    {
        _gameId = gameId;
    }

    public void Execute()
    {
        var cmd = IoC.Resolve<ICommand>("RuleInicializationGameCommand");
        IoC.Resolve<ICommand>("Game.Queue.Push", _gameId, cmd).Execute();
    }
}
