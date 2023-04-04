namespace SpaceBattle.Lib.Test;

public class GameQueuePushStrategyTest
{
    [Fact]
    public void SuccessfulGameQueuePushStrategy()
    {
        var gameQueuePushStrategy = new GameQueuePushStrategy();

        Assert.NotNull(gameQueuePushStrategy.DoAlgorithm("1", new Mock<ICommand>().Object));
    }
}
