using Game;
using System.Diagnostics.CodeAnalysis;
[ExcludeFromCodeCoverage]
class Server
{
    static void Main()
    {
        Dictionary<string, object> SetOfProperties = new Dictionary<string, object>()
        {
            ["position"] = new Vector(12, 5),
            ["velocity"] = new Vector(-7, 3),
            ["angle"] = 45,
            ["angle velocity"] = 90
        };
        var ob = new UObject(SetOfProperties);
        var o1 = new MovableAdapter(ob);
        string a = "position";
        object o = 1; 
        ob.setProperty(a, o);
        //Moving.Move(o1);
        //Rotating.Rotate(new RotatableAdapter(ob));
        foreach (var i in ob.SetOfProperties)
        {
            Console.WriteLine($"{i.Key} : {i.Value}");
        }

    }
}