namespace SpaceBattle.Lib;
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