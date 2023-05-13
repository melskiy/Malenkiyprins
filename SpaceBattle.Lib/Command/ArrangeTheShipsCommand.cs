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
        var Enum = IoC.Resolve<IEnumerator<object>>("EnemiesPositionEnum");
        foreach (var i in ships)
        {
            IoC.Resolve<ICommand>("SetEnemyPosition", Enum, i).Execute();
        }
        Enum.Reset();
    }
}
