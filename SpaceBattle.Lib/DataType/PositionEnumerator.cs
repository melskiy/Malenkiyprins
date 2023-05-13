using System.Collections;
namespace SpaceBattle.Lib;
using Hwdtech;
public class PositionEnumerator : IEnumerator<object>
{
    private IList<Vector> positions;
    private int currentship;
    private int delta;
    public PositionEnumerator()
    {
        this.positions = IoC.Resolve<IList<Vector>>("GetShipPositions");
        this.currentship = IoC.Resolve<int>("GetCurrentShip");
        this.delta = IoC.Resolve<int>("DistanceBetwinShips");
    }
    public object Current
    {
        get
        {
            return positions[this.currentship] + new Vector(new int[2] { delta, 0 });
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public bool MoveNext()
    {
        if (this.currentship < positions.Count() - 1)
        {
            this.currentship++;
            return true;
        }
        return false;
    }
    public void Reset() => IoC.Resolve<ICommand>("SetCurrentShip", currentship + 1).Execute();
}
