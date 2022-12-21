namespace SpaceBattle.Lib;
public class MoveCommandTest
{
    [Fact]
    public void MoveCommandTestPositive()
    {
        Mock<IMovable> movable = new Mock<IMovable>();
        movable.SetupProperty(i => i.Position, new Vector(12, 5));
        movable.SetupGet<Vector>(i => i.Velocity).Returns(new Vector(-7, 3));
        MoveCommand command = new MoveCommand(movable.Object);
        command.Execute();
        Assert.Equal(new Vector(5, 8), movable.Object.Position);
    }
    [Fact]
    public void MoveCommandTestGetVelocityExeption()
    {
        Mock<IMovable> movable = new Mock<IMovable>();
        movable.SetupProperty(i => i.Position, new Vector(12, 5));
        movable.SetupGet<Vector>(i => i.Velocity).Throws<ArgumentException>();
        MoveCommand command = new MoveCommand(movable.Object);
        Assert.Throws<ArgumentException>(() => command.Execute());
    }
    [Fact]
    public void MoveCommandTestGetPositionExeption()
    {
        Mock<IMovable> movable = new Mock<IMovable>();
        movable.SetupProperty(i => i.Position, new Vector(12, 5));
        movable.SetupGet<Vector>(i => i.Position).Throws<ArgumentException>();
        movable.SetupGet<Vector>(i => i.Velocity).Returns(new Vector(-7, 3));
        MoveCommand command = new MoveCommand(movable.Object);
        Assert.Throws<ArgumentException>(() => command.Execute());
    }
    [Fact]
    public void MoveCommandTestSetPositionExeption()
    {
        Mock<IMovable> movable = new Mock<IMovable>();
        movable.SetupProperty(i => i.Position, new Vector(12, 5));
        movable.SetupSet(i => i.Position = It.IsAny<Vector>()).Throws<ArgumentException>();
        movable.SetupGet<Vector>(i => i.Velocity).Returns(new Vector(-7, 3));
        MoveCommand command = new MoveCommand(movable.Object);
        Assert.Throws<ArgumentException>(() => command.Execute());
    }
}
