namespace Game;
using System;

public interface IUobject
{
    public void setProperty(string key, object value);
    public object getProperty(string key);
}
public interface IMovable
{
    public void setPosition(Vector pos);
    public Vector getPosition();
    public Vector getVelocity();
}
public interface IRotatable
{
    public void setAngle(double ang);
    public double getAngle();
    public double getAngleVelosity();
    public Vector getVelocity();
    public void setVelosity(Vector vel);
    public Vector getPosition();
}
public class UObject : IUobject
{
    public Dictionary<string, object> SetOfProperties = new Dictionary<string, object>() { };
    public UObject(Dictionary<string, object> dic)
    {
        SetOfProperties = dic;
    }
    public void setProperty(string key, object value)
    {
        SetOfProperties.Add(key, value);
    }
    public object getProperty(string key)
    {
        return SetOfProperties[key];
    }
    // public override string ToString()
    // {

    //     // foreach(var i in SetOfProperties)
    //     // {
    //     //     if(i.Key != null && i.Value != null){
    //     //     return $"key: [{i.Key}], value: [{i.Value}]";
    //     //     }
    //     //     return "пусто";
    //     // }
    // }

};
public class MovableAdapter : IMovable
{
    private IUobject uobj;
    public MovableAdapter(IUobject ob)
    {
        this.uobj = ob;
    }
    public void setPosition(Vector o)
    {
        string a = "position";
        uobj.setProperty(a, (object)o);
    }
    public Vector getPosition()
    {
        return (Vector)uobj.getProperty("position");
    }
    public Vector getVelocity()
    {
        return (Vector)uobj.getProperty("velocity");
    }
};
public class RotatableAdapter : IRotatable
{
    private IUobject uobj;
    public RotatableAdapter(IUobject ob)
    {
        this.uobj = ob;
    }
    public void setAngle(double ang)
    {
        string a = "angle";
        uobj.setProperty(a, (object)ang);
    }
    public double getAngle()
    {
        return (double)uobj.getProperty("angle");
    }
    public double getAngleVelosity()
    {
        return (double)uobj.getProperty("angle velocity");
    }
    public Vector getVelocity()
    {
        return (Vector)uobj.getProperty("velocity");
    }
    public void setVelosity(Vector o)
    {
        string a = "velocity";
        uobj.setProperty(a, (object)o);
    }
    public Vector getPosition()
    {
        return (Vector)uobj.getProperty("position");
    }
};
public class Moving
{
    public static void Move(IMovable a)
    {

        if (a.getPosition().Size != a.getVelocity().Size)
        {
            throw new ArgumentException("Не совпадают размерности векторов position и velocity");
        }
        Vector help;
        help = a.getPosition();
        for (int i = 0; i < help.Size; i++)
        {
            help[i] += a.getVelocity()[i];
        }
        a.setPosition(help);
    }
}
public class Rotating
{
    public static void Rotate(IRotatable a)
    {
        double newangle = (a.getAngle() + a.getAngleVelosity()) % 360;
        a.setAngle(newangle);
        double angleRadian = a.getAngleVelosity() * Math.PI / 180;

        //хранить угол как рациональную дробь m/n

        int xnew = (int)(a.getPosition()[0] * Math.Cos(angleRadian) - a.getPosition()[1] * Math.Sin(angleRadian));
        int ynew = (int)(a.getPosition()[0] * Math.Sin(angleRadian) + a.getPosition()[1] * Math.Cos(angleRadian));
        Vector v = new Vector(xnew, ynew);
        a.setVelosity(v);

        // int maxDirections = (int) (2 * Math.PI / (a.getAngelVelocity() * 180 / Math.PI)); //максимальное количество направлений;
        // int velocityDirection = 1;//угловая скорость, выраженная в направлении
        // double rotate;
        // int direction = 0;
        // rotate = (direction + velocityDirection) % maxDirections;
        // double alpha = 2 * Math.PI / maxDirections * direction;

        // int xnew = (int)(a.getVelocity()[0] * Math.Cos(a.getAngle()) - a.getVelocity()[1] * Math.Sin(a.getAngle()));
        // int ynew = (int)(a.getVelocity()[0] * Math.Sin(a.getAngle()) + a.getVelocity()[1] * Math.Cos(a.getAngle()));
        // int[] v = { xnew, ynew };
        // a.setVelosity(v);
    }
}