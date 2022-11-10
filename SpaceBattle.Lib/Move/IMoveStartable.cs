namespace SpaceBattle.Lib;
public interface IMoveStartable
{
    UObject Target
    {
        get;
    }
    Vector InitialVelocity
    {
        get;
    }
}