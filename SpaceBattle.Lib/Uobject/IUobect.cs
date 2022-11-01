namespace SpaceBattle.Lib;
public interface IUobject
{
    public void setProperty(string key, object value);
    public object getProperty(string key);
}