namespace SpaceBattle.Lib;
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