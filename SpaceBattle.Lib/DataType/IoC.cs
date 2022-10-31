namespace SpaceBattle.Lib;
public class IoC
{
    private static Dictionary<string, IStrategy> store = new();
    public static void Resolve<T>(string key, params object[] args) => store[key].Execute();
}