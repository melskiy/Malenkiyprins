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
        IoC.Resolve<ICommand>("Game.Queue.Push", IoC.Resolve<IQueue<ICommand>>("Game.Queue"), cmd).Execute();
    }

}


// public class RepeatCommandStrategy : IStrategy
// {
//     public object DoAlgorithm(params object[] args)
//     {
        
//     }
// }