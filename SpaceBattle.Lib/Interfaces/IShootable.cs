namespace SpaceBattle.Lib;

public interface IShootable
{
    public string ProjectileType 
    {
        get; 
        set;
    }
    public Vector Position
    {
        get;
        set;
    }
    public Vector Velocity
    {
        get;
    }
}
