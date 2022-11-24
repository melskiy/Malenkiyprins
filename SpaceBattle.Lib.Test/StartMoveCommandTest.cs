namespace SpaceBattle.Lib.Test;

public class StartMoveCommandTests
{
    [Fact]
    public void PositiveStartMoveTest()
    {
        IoC.Resolve<ICommand>("IoC.Add", "Game.OfTwo");
       
    }
}