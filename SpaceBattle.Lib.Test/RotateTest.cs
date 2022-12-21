namespace SpaceBattle.Lib.Test;
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
