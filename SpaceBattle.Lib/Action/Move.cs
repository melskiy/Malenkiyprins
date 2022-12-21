namespace SpaceBattle.Lib;

public class MoveCommand : ICommand
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
