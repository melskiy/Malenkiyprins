namespace SpaceBattle.Lib.Test;
public class MoveCommandTest
{
    [Fact]
    public void MoveCommandTestPositive()
    {
        var movable = new Mock<IMovable>();
        movable.Setup(i=>i.Position).Returns(new Vector(12, 5)).Verifiable();
        movable.Setup(i=>i.Velocity).Returns(new Vector(-7, 3)).Verifiable();
        var c = new MoveCommand(movable.Object);
        c.Execute();
        movable.VerifySet(i=>i.Position = new Vector(5, 8), Times.Once);
    }
}
public class RotateCommandTest
{
    [Fact]
    public void RotateCommandTestPositive()
    {
        var movable = new Mock<IRotatable>();
        movable.Setup(i=>i.Angle).Returns(45).Verifiable();
        movable.Setup(i=>i.AngleVelocity).Returns(90).Verifiable();
        var c = new RotateCommand(movable.Object);
        c.Execute();
        movable.VerifySet(i=>i.Angle = 135, Times.Once);
    }
}