
namespace SpaceBattle.Lib;
using Hwdtech;
using System.Collections.Concurrent;

public class StopServerStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
       return new StopServerCommand();
    }
}
