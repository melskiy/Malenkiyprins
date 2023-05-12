namespace SpaceBattle.Lib.Test;

public class GetCommandFromQueueStrategyTests
{
    [Fact]
    public void SuccessfulGetCommandFromQueueStrategy()
    {
        var cmd1 = new Mock<ICommand>();
        var queue = new Queue<ICommand>();

        queue.Enqueue(cmd1.Object);

        var getCommandFromQueueStrategy = new GetCommandFromQueueStrategy();

        var cmd2 = getCommandFromQueueStrategy.DoAlgorithm(queue);

        Assert.True(queue.Count() == 0);
        Assert.True(cmd2 == cmd1.Object);
    }

    [Fact]
    public void UnsuccessfulGetCommandFromQueueStrategy()
    {
        var cmd = new Mock<ICommand>();
        var queue = new Queue<ICommand>();
        var getCommandFromQueueStrategy = new GetCommandFromQueueStrategy();

        Assert.Throws<Exception>(() => getCommandFromQueueStrategy.DoAlgorithm(queue));
    }
}
