namespace SpaceBattle.Lib.Test;
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
public class RotateCommandTest
{
    [Fact]
    public void RotateCommandTestPositive()
    {
        Mock<IRotatable> movable = new Mock<IRotatable>();
        movable.SetupProperty(i => i.Angle, new Angle(45, 1));
        movable.SetupGet<Angle>(i => i.AngleVelocity).Returns(new Angle(90, 1));
        RotateCommand command = new RotateCommand(movable.Object);
        command.Execute();
        Assert.Equal(new Angle(135, 1), movable.Object.Angle);

    }
    [Fact]
    public void RotateCommandTestGetAngleVelocityExeption()
    {
        Mock<IRotatable> movable = new Mock<IRotatable>();
        movable.SetupProperty(i => i.Angle, new Angle(45, 1));
        movable.SetupGet<Angle>(i => i.AngleVelocity).Throws<ArgumentException>();
        RotateCommand command = new RotateCommand(movable.Object);
        Assert.Throws<ArgumentException>(() => command.Execute());

    }

    [Fact]
    public void RotateCommandTestGetAngleExeption()
    {
        Mock<IRotatable> movable = new Mock<IRotatable>();
        movable.SetupProperty(i => i.Angle, new Angle(45, 1));
        movable.SetupGet<Angle>(i => i.Angle).Throws<ArgumentException>();
        movable.SetupGet<Angle>(i => i.AngleVelocity).Returns(new Angle(90, 1));
        RotateCommand command = new RotateCommand(movable.Object);
        Assert.Throws<ArgumentException>(() => command.Execute());

    }
    [Fact]
    public void RotateCommandTestSetPositionExeption()
    {
        Mock<IRotatable> movable = new Mock<IRotatable>();
        movable.SetupProperty(i => i.Angle, new Angle(45, 1));
        movable.SetupSet(i => i.Angle = It.IsAny<Angle>()).Throws<ArgumentException>();
        movable.SetupGet<Angle>(i => i.AngleVelocity).Returns(new Angle(90, 1));
        RotateCommand command = new RotateCommand(movable.Object);
        Assert.Throws<ArgumentException>(() => command.Execute());

    }
}