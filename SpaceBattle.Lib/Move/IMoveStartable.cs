namespace SpaceBattle.Lib;
public interface IMoveStartable
{   IUObject Target
    {
        get;
    }
    IDictionary<string, object> Properties
    {
        get;
    }
}
