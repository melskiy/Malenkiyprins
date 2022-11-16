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
        var listcommand = listcommands.Select(c => IoC.Resolve<ICommand>(c, uobj));

        ICommand _TAkeinMouse267e_9 = new Moving(uobj);

        ICommand repeat = IoC.Resolve<ICommand>("Create.RepeatCommand", _TAkeinMouse267e_9);
        listcommand.Append(repeat);

        ICommand macro = IoC.Resolve<ICommand>("Create.MacroCommand", listcommand);
        IoC.Resolve<ICommand>("Game.Commands.SetProperty", uobj, "ThisCommand", macro).Execute();
        return _TAkeinMouse267e_9;
    }


}
public class CreateMacroCommand : IStrategy
{

    public object DoAlgorithm(params object[] args)
    {
        return new MacroCommand((IEnumerable<ICommand>)args[0]);
    }
}
public class CreateRepeatCommand : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        return new RepeatCommand((ICommand)args[0]);
    }

}
public class Moving : ICommand
{
    private IUObject obj;
    public Moving(IUObject obj)
    {
        this.obj = obj;
    }

    public void Execute()
    {
        ((ICommand)obj.getProperty("ThisCommand")).Execute();
    }
}