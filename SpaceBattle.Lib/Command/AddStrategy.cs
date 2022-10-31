public class IoCAddStrategy : IStrategy
{
    Dictionary<string, IStrategy> store = new();
    string key; 
    IStrategy strategy;
    IoCAddStrategy(string key, IStrategy strategy){
        this.key = key;
        this.strategy = strategy;
    }
    public void Execute()
    {
        this.store[key] = strategy;     
    }
}

