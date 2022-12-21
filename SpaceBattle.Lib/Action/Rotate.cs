namespace SpaceBattle.Lib;

public class RotateCommand : ICommand
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
