namespace SpaceBattle.Lib.Test;

public class GameRuleRegistrationStrategyTest
{
    [Fact]
    public void SuccessfulGameRuleRegistrationStrategyRunStrategy()
    {
        var id = "id";

        var registrationRuleStrategy = new RegistrationGameRulesStrategy();

        var cmd = registrationRuleStrategy.DoAlgorithm(id);

        Assert.NotNull(cmd);
        Assert.True(cmd.GetType() == typeof(RegistrationGameRulesCommand));
    }
}
