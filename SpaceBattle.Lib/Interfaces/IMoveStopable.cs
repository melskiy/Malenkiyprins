namespace SpaceBattle.Lib;
public interface IMoveStopable
{
    IUObject Target
    {
        get;
    }
    IEnumerable<string> Properties
    {
        get;
    }
}
