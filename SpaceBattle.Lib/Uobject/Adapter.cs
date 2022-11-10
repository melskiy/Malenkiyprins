namespace SpaceBattle.Lib;
//System refllection;
//Activator
//Imovable movable =IoC.Resolve<IMovable>("GenerateAdapter", typeOf(IMovable), object);  стратегия

public class MovableAdapter : IMovable
{
    private IUObject obj;
    MovableAdapter(IUObject obj)
    {
        this.obj = obj;
    }
    public Vector Position
    {
        get
        {
            return (Vector)IoC.Resolve<object>("UObject.getProperty", this.obj, "Position");
        }
        set
        {
            IoC.Resolve<ICommand>("UObject.setProperty", this.obj, "Position", value);
        }
    }
    public Vector Velocity
    {
        get
        {
            return (Vector)IoC.Resolve<object>("UObject.getProperty", this.obj, "Velocity");
        }
    }


}