using Hwdtech;
namespace SpaceBattle.Lib;

public class HandleCommand : ICommand
{
    private IMessage obj;
    public HandleCommand(IMessage obj)
    {
        this.obj = obj;
    }
    public void Execute()
    {
        var thread = IoC.Resolve<IDictionary<string, int>>("Game-Thread");
        IoC.Resolve<ICommand>("SendCommandStrategy", thread[(string)obj.GameID], IoC.Resolve<ICommand>("GameCreateCommandStrategy", obj)).Execute();
    }
}
