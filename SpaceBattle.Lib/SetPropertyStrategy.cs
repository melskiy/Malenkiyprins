namespace SpaceBattle.Lib;
public class SetPropertyStrategy: IStrategy
{
       public object DoAlgorithm(params object[] args)
       {
            IUObject obj =  (IUObject)args[0];
            string key = (string)args[1];
            object par = (object)args[2];
            return new SetPropertyCommand(obj, key, par);
       }
}

public class SetPropertyCommand: ICommand
{
    private IUObject obj;
    private string key;
    private object values;
    public SetPropertyCommand(IUObject obj, string key, object values)
    {
        this.obj = obj;
        this.key = key;
        this.values = values;
    }
    public void Execute()
    {
        obj.setProperty(key, values);
    }
}