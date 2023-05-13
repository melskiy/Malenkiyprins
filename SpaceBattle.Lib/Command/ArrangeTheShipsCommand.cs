namespace SpaceBattle.Lib;
using Hwdtech;

public class ArrangeTheShipsCommand : ICommand
{
    private IList<string> ships;
    public ArrangeTheShipsCommand(IList<string> ships)
    {
        this.ships = ships;
    }
    public void Execute()
    {
        var delta = IoC.Resolve<int>("DistanceBetwinShips");
        var map = IoC.Resolve<IDictionary<string, IUObject>>("GetUObjects");
        var corrent = IoC.Resolve<int>("GetCurrentShip");
        Vector position = new Vector(0, 0);
        var newpos = new PositionEnumerator();

        foreach (var i in ships)
        {
            position = (Vector)newpos.Current;
            IoC.Resolve<ICommand>("GameUObjectSetPropertyStrategy", map[i], "position", position).Execute();
            newpos.MoveNext();
        }
         newpos.Reset();
    }
}
