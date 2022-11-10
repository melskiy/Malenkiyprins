namespace SpaceBattle.Lib;
public class RepeatCommand : ICommand
{
    private ICommand cmd;
    public RepeatCommand(ICommand cmd)
    {
        this.cmd = cmd;
    }

    public void Execute()
    {
        cmd.Execute();
        //TODO: запушить в очередь
    }

}