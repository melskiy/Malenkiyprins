namespace SpaceBattle.Lib;
public interface IMoveStartable
{
    IUObject Target
    {
        get;
    }
    Vector InitialVelocity
    {
        get;
    }
}