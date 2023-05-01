using Hwdtech;
namespace SpaceBattle.Lib;

public class GameScopeStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var gameid = (string)args[0];
        var parentscope = (object)args[1];
        var quantum = (double)args[2];
        return new GameScopeCommand(gameid, parentscope, quantum);
    }
}
