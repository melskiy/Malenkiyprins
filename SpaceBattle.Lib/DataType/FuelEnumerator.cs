using System.Collections;
namespace SpaceBattle.Lib;
using Hwdtech;

public class FuelEnumerator : IEnumerator<object>
{
    private IList<int> fuels;
    private int index = 0;
    public FuelEnumerator()
    {
        this.fuels = IoC.Resolve<IList<int>>("GetFuels");
        this.index = IoC.Resolve<int>("GetFuelIndex");
    }
    public object Current
    {
        get
        {
            return fuels[index];
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public bool MoveNext()
    {
        if (this.index < fuels.Count() - 1)
        {
            this.index++;
            return true;
        }
        return false;
    }
    public void Reset() => IoC.Resolve<ICommand>("SetFuelIndex", index + 1).Execute();
}
