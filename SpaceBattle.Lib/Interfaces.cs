namespace SpaceBattle.Lib;
public interface ICommand
{
    void Execute();
}


public interface IMovable
{
    public Vector Position
    {
        get;
        set;
    }
    public Vector Velocity
    {
        get;
    }
}


public interface IRotatable
{
    public Angle Angle
    {
        get;
        set;
    }
    public Angle AngleVelocity
    {
        get;
    }
}