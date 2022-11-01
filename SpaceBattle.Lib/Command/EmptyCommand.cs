namespace SpaceBattle.Lib;
public class EmptyCommand : ICommand
{
    public void Execute() { }
}


public class EmptyCommandStrategy : IStrategy
{
    private ICommand command;
    public EmptyCommandStrategy()
    {
        this.command = new EmptyCommand();
    }
    public object Execute(params object[] args)
    {
        return this.command;
    }
}