namespace SpaceBattle.Lib;

public class GameUObjectSetPropertyCommand: ICommand
{
    IUObject _obj;
    string _key;
    object _value;

    public GameUObjectSetPropertyCommand(IUObject obj, string key, object value)
    {
        _obj = obj;
        _key = key;
        _value = value;
    }

    public void Execute()
    {
        _obj.setProperty(_key, _value);
    }
}
