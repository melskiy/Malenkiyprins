namespace SpaceBattle.Lib;

public class RegistrationGameRulesStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var gameId = (string)args[0];

        return new RegistrationGameRulesCommand(gameId);
    }
}
