using Moq;

namespace SpaceBattle.Lib.Test;

public class GameUObjectSetPropertyCommandTest
{
    [Fact]
    public void SuccessfulGameUObjectSetPropertyCommand()
    {
        var obj = new Mock<IUObject>();
        obj.Setup(o => o.setProperty(It.IsAny<string>(), It.IsAny<object>())).Callback(() => {}).Verifiable();
        var gameUObjectSetPropertyCommand = new GameUObjectSetPropertyCommand(obj.Object, "key", "value");

        gameUObjectSetPropertyCommand.Execute();

        obj.VerifyAll();
    }
}
