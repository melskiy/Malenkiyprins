namespace SpaceBattle.Lib;
public class UObject : IUobject
{
    public Dictionary<string, object> SetOfProperties = new Dictionary<string, object>() { };
    public UObject(Dictionary<string, object> dic)
    {
        SetOfProperties = dic;
    }
    public void setProperty(string key, object value)
    {
        SetOfProperties.Add(key, value);
    }
    public object getProperty(string key)
    {
        return SetOfProperties[key];
    }


};