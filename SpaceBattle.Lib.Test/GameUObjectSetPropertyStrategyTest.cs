namespace SpaceBattle.Lib.Test;

public class GameUObjectSetPropertyStrategyTest
{
    [Fact]
    public void SuccessfulGameUObjectSetPropertyStrategy()
    {
        var gameUObjectSetPropertyStrategy = new GameUObjectSetPropertyStrategy();

        Assert.NotNull(gameUObjectSetPropertyStrategy.DoAlgorithm(new Mock<IUObject>().Object, "key", "value"));
    }
}
