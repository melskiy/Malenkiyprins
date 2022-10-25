namespace SpaceBattle.Lib;

public class StartMoveCommand:ICommand{
    private IMovable obj;
    public StartMoveCommand(IMovable obj)
    {
        this.obj = obj;
    }
    public void Execute()
    {
        var MoveCommand = new MoveCommand(obj);
        IoC.Resolve<object>("qadd",MoveCommand);
        MoveCommand.Execute();
    }
}