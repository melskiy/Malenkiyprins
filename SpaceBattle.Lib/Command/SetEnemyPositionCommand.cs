namespace SpaceBattle.Lib;
using Hwdtech;

public class SetEnemyPositionCommand: ICommand
{
    private IEnumerator<object> Enum;
    private string ship;
    private Dictionary<string,IUObject> map;
    public SetEnemyPositionCommand(IEnumerator<object> Enum, string ship)
    {
        this.Enum = Enum;
        this.ship = ship;
        this.map = IoC.Resolve<Dictionary<string,IUObject>>("GetUObjects");
    }
    public void Execute()
    {
        IoC.Resolve<ICommand>("GameUObjectSetPropertyStrategy", this.map[ship], "position", this.Enum.Current).Execute();
        Enum.MoveNext();
    }
}
