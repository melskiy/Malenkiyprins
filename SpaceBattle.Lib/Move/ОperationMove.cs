namespace SpaceBattle.Lib;

public class MoveStrategy : IStrategy
{

    public object DoAlgorithm(params object[] args)
    {
        
        IUObject uObject = (IUObject)args[0];
        IMovable movable = IoC.Resolve<IMovable>("Game.Adapter", uObject, typeof(MovableAdapter));
        ICommand cmd = new MoveCommand(movable);
        return cmd;
    }
}

public class OperationMoveStrategy : IStrategy
{

    public object DoAlgorithm(params object[] args)
    {
        IUObject uobj = (IUObject)args[0];
        IEnumerable<string> listcommands = (IEnumerable<string>)args[1];
        var listcommand = listcommands.Select(c => IoC.Resolve<ICommand>(c,uobj));
        ICommand mooving =  IoC.Resolve<ICommand>("Create.MacroCommand", listcommand);
        IoC.Resolve<ICommand>("Game.Commands.SetProperty",uobj,"ThisCommand",mooving).Execute();
        ICommand rmooving = IoC.Resolve<ICommand>("Create.RepeatCommand", uobj.getProperty("ThisCommand"));
        return rmooving;
    }
    

}
public class CreateMacroCommand : IStrategy {
    
    public object DoAlgorithm(params object[] args)
    {
       return new MacroCommand((IEnumerable<ICommand>)args[0]);
    }
}
public class CreateRepeatCommand : IStrategy{
    public object DoAlgorithm(params object[] args)
    {
       return new RepeatCommand((ICommand)args[0]);
    }

}