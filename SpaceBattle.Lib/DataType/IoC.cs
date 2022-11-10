namespace SpaceBattle.Lib;
public class IoC
{
    private static Dictionary<string, IStrategy> store;
    static IoC()
    {
        store = new Dictionary<string, IStrategy>();
        store["IoC.Add"] = new IoCAddStrategy(store);
        store["IoC.Resolve"] = new IoCResolveStrategy(store);
    }
    public static T Resolve<T>(string key, params object[] args) => (T)store["IoC.Resolve"].DoAlgorithm(key, args);
}


public class IoCAddCommand : ICommand
{
    private IDictionary<string, IStrategy> store;
    private string key;
    private IStrategy strategy;
    public IoCAddCommand(IDictionary<string, IStrategy> store, string key, IStrategy strategy)
    {
        this.store = store;
        this.key = key;
        this.strategy = strategy;
    }
    public void Execute()
    {
        this.store[this.key] = this.strategy;
    }
}


public class IoCAddStrategy : IStrategy
{
    private IDictionary<string, IStrategy> store;
    public IoCAddStrategy(IDictionary<string, IStrategy> store)
    {
        this.store = store;
    }
    public object DoAlgorithm(params object[] args)
    {
        string key = (string)args[0];
        IStrategy strategy = (IStrategy)args[1];
        return new IoCAddCommand(this.store, key, strategy);
    }
}


public class IoCResolveStrategy : IStrategy
{
    private Dictionary<string, IStrategy> store;
    public IoCResolveStrategy(Dictionary<string, IStrategy> store)
    {
        this.store = store;
    }
    public object DoAlgorithm(params object[] args)
    {
        string key = (string)args[0];
        object[] par = (object[])args[1];
        return store[key].DoAlgorithm(par);
    }
}
