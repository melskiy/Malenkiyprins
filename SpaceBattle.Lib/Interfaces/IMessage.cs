namespace SpaceBattle.Lib;

public interface IMessage
{
    public string OrderType
    {
        get;
    }

    public string GameID
    {
        get;
    }

    public string GameItemID
    {
        get;
    }

    public IDictionary<string, object> Properties
    {
        get;
    }
}
