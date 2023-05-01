namespace SpaceBattle.Lib;
using Hwdtech;
public class GameDeleteCommand : ICommand
{
    private string gameid;
    public GameDeleteCommand(string gameid)
    {
        this.gameid = gameid;
    }

    public void Execute()
    {
        var GameCommandMap = IoC.Resolve<IDictionary<string, Iinjectable>>("GameCommandMap");
        var GameCommand = GameCommandMap[gameid];
        GameCommand.Inject(IoC.Resolve<ICommand>("Game.Commands.EmptyCommand"));
        var GameScopeMap = IoC.Resolve<IDictionary<string, object>>("GameScopeMap");
        GameScopeMap.Remove(gameid);
    }
}
