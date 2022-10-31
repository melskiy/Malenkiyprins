namespace SpaceBattle.Lib;
public class StartMoveCommand : ICommand
{
    IUobject _uObject;

    public StartMoveCommand(IUobject obj)
    {
        this._uObject = obj;
    }

    // public StartMoveCommand(IMovableStartable o) {}

    public void Execute()
    {
        // ICommand rc = IoC<ICommand>.Resolve("Command.Repeat", this._queue);
        // IoC<ICommand>.Resolve("UObject.setProperty", this._uObject, "Command.Move", rc);
        // this._queue.Push(rc);
    }
}
