using Hwdtech;
using Hwdtech.Ioc;

namespace SpaceBattle.Lib.Test;

public class GameQueuePushCommandTest
{
    [Fact]
    public void SuccessfulGameQueuePushCommand()
    {
        var queue = new Queue<ICommand>();
        var cmd = new Mock<ICommand>();
        var gameQueuePushCommand = new GameQueuePushCommand(queue, cmd.Object);

        gameQueuePushCommand.Execute();

        Assert.True(queue.Count == 1);
    }
}
