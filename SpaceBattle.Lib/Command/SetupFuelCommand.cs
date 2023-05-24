namespace SpaceBattle.Lib;
using Hwdtech;

public class SetupFuelCommand : ICommand
{
    private IList<string> listship;

    public SetupFuelCommand(IList<string> listship)
    {
        this.listship = listship;
    }
    public void Execute()
    {
        var map = IoC.Resolve<IDictionary<string, IUObject>>("GetUObjects");
        var Enum = IoC.Resolve<IEnumerator<object>>("FuelEnumerator");
        foreach (var ship in listship)
        {
            IoC.Resolve<ICommand>("SetFuel", Enum, ship).Execute();
        }
        Enum.Reset();
    }
}
