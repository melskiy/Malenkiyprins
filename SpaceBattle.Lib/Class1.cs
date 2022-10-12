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
    public int Angle 
    {
        get;
        set;
    }
    public int AngleVelocity 
    {
        get;
    }
}
public class MoveCommand: ICommand
{
    private IMovable obj;
    public MoveCommand(IMovable obj)
    {
        this.obj = obj;
    }
    public void Execute()
    {
        obj.Position += obj.Velocity;
    }
}
public class RotateCommand: ICommand
{
    private IRotatable obj;
    public RotateCommand(IRotatable obj)
    {
        this.obj = obj;
    }
    public void Execute()
    {
        obj.Angle += obj.AngleVelocity; 
    }
}
