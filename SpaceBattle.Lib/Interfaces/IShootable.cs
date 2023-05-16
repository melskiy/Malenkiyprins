namespace SpaceBattle.Lib;

public interface IShootable
{
    public IDictionary<string, int> Ammunition
    {
        get;
        set;
    }
}
