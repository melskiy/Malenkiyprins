namespace SpaceBattle.Lib;
using Hwdtech;
using System.Collections.Concurrent;

public class StartServerStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
       return new StartServer((int)args[0]);
    }
}
