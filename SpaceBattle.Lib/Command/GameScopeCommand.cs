namespace SpaceBattle.Lib;
using Hwdtech;
public class GameScopeCommand : ICommand
{

    private string gameid;
    private object parentscope;

    private double quantum;

    public GameScopeCommand(string gameid, object parentscope, double quantum)
    {
        this.gameid = gameid;
        this.parentscope = parentscope;
        this.quantum = quantum;
    }

    public void Execute()
    {
        var GameScopeMap = IoC.Resolve<IDictionary<string, object>>("GameScopeMap");
        var scope = IoC.Resolve<object>("Scopes.New", parentscope);
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", scope).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetQuantum", (object[] args) => quantum).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameQueuePushStrategy", (object[] args) => new GameQueuePushStrategy().DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameQueuePopStrategy", (object[] args) => new GameQueuePopStrategy().DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUObjectFromObjectMapStrategy", (object[] args) => new GetUObjectFromObjectMapStrategy().DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "DeleteUObjectFromObjectMapStrategy", (object[] args) => new DeleteUObjectFromObjectMapStrategy().DoAlgorithm(args)).Execute();
        GameScopeMap.Add(gameid, scope);
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", parentscope).Execute();
    }
}
