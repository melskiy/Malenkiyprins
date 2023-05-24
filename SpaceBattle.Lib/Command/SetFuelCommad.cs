namespace SpaceBattle.Lib;
using Hwdtech;

public class SetFuelCommad: ICommand
{
    private IEnumerator<object> Enum;
    private string ship;
    private Dictionary<string,IUObject> map;
    public SetFuelCommad(IEnumerator<object> Enum, string ship)
    {
        this.Enum = Enum;
        this.ship = ship;
        this.map = IoC.Resolve<Dictionary<string,IUObject>>("GetUObjects");
    }
    public void Execute()
    {
        IoC.Resolve<ICommand>("GameUObjectSetPropertyStrategy", this.map[ship], "fuel", this.Enum.Current).Execute();
        Enum.MoveNext();
    }
}
