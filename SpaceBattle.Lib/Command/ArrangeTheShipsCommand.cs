namespace SpaceBattle.Lib;
using Hwdtech;

public class ArrangeTheShipsCommand : ICommand
{
    private IList<string> listship;
    private IList<string> listenemy;
    public ArrangeTheShipsCommand(IList<string> listship, IList<string> listenemy)
    {
        this.listship = listship;
        this.listenemy = listenemy;
    }
    public void Execute()
    {
        var delta = IoC.Resolve<int>("DistanceBetwinShips");
        var map = IoC.Resolve<IDictionary<string, IUObject>>("GetUObjects");

        listenemy.ToList().ForEach(
         x =>
         {
             IoC.Resolve<ICommand>("GameUObjectSetPropertyStrategy", map[x], "position", (Vector)(map[listship[listenemy.IndexOf(x)]].getProperty("position")) + new Vector(new int[2] { delta, 0 })).Execute();
         });

    }
}
