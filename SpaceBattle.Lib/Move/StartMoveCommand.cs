namespace SpaceBattle.Lib;
public class StartMoveCommand : ICommand
{
    IMoveStartable startable;
    IEnumerable<string> listcomands;

    public StartMoveCommand(IMoveStartable startable, IEnumerable<string> listcomands)
    {
        this.startable = startable;
        this.listcomands = listcomands;
    }

    public void Execute()
    {
        IoC.Resolve<ICommand>(
            "Game.Commands.SetProperty",
            startable.Target,
            "Velocity",
            startable.InitialVelocity
        ).Execute();

        ICommand cmd  = IoC.Resolve<ICommand>("Game.Commands.Move", startable.Target,listcomands);
        IoC.Resolve<ICommand>("Game.Queue.Push", IoC.Resolve<IQueue<ICommand>>("Game.Queue"), cmd).Execute();
        
    }

}
