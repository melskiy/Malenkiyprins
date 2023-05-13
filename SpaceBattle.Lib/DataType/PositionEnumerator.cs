using System.Collections;
namespace SpaceBattle.Lib;
using Hwdtech;
class PositionEnumerator : IEnumerator
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
            return positions[this.currentship] + new Vector(new int[2]{delta,0});
        }
    }
    public bool MoveNext()
    {
        if (this.currentship < positions.Count())
        {
            currentship++;
            return true;
        }
        else
            return false;
    }
    public void Reset() => currentship = currentship +1;
}
