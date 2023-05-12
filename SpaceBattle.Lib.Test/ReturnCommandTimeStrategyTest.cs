namespace SpaceBattle.Lib.Test;

public class ReturnCommandTimeStrategyTest
{
    [Fact]
    public void SuccessfulReturnCommandTimeStrategy()
    {
        var queue = new Queue<ICommand>();
        var strategy = new ReturnCommandTimeStrategy();

        Assert.NotNull(strategy.DoAlgorithm(queue));
    }
}
