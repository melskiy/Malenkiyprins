namespace SpaceBattle.Lib;
public interface IMoveStartable
{
    UObject Target
    {
        get;
    }
    int InitialVelocity
    {
        get;
    }
}