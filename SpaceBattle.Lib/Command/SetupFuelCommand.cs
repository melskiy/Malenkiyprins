namespace SpaceBattle.Lib;
using Hwdtech;

public class SetupFuelCommand : ICommand
{
    private IList<string> listship;
    private IList<int> listfuel;

    public SetupFuelCommand(IList<string> listship, IList<int> listfuel)
    {
        this.listship = listship;
        this.listfuel = listfuel;
    }
    public void Execute()
    {
        var map = IoC.Resolve<IDictionary<string, IUObject>>("GetUObjects");

        listship.ToList().ForEach(
            x => IoC.Resolve<ICommand>("GameUObjectSetPropertyStrategy", map[x], "fuel",
                listfuel[listship.IndexOf(x)]).Execute()
         );
    }
}
