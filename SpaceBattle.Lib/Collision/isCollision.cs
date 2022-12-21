using Hwdtech;
namespace SpaceBattle.Lib;

public class IsCollision : ICommand
{
    private IUObject obj1, obj2;
    public IsCollision(IUObject obj1, IUObject obj2)
    {
        this.obj1 = obj1;
        this.obj2 = obj2;
    }
    public void Execute()
    {
        var Tree = IoC.Resolve<IDictionary<int, object>>("ICollisionTreeRootDictionary");
        var prop1 = IoC.Resolve<List<int>>("Game.Commands.GetCollicionPropertys", obj1);
        var prop2 = IoC.Resolve<List<int>>("Game.Commands.GetCollicionPropertys", obj2);
        List<int> ListofVectors = IoC.Resolve<List<int>>("PrepareData", prop1, prop2);
        ListofVectors.ForEach(num => Tree = (IDictionary<int, object>)Tree[num]);
        if (Tree.Keys.First() == 1)
        {
            IoC.Resolve<ICommand>("Game.Colision", obj1, obj2).Execute();
        }
    }
}
