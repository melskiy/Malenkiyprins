namespace SpaceBattle.Lib;
public interface IStartable
{   IUObject Target
    {
        get;
    }
    IDictionary<string, object> Properties
    {
        get;
    }
}