namespace SpaceBattle.Lib;
using System;
using System.IO;
using System.Reflection;
using Hwdtech;
public class FindAdapterStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var type1 = (Type)args[0];
        var obj = (object)args[1];
        var ms = IoC.Resolve<IDictionary<string,MemoryStream>>("GetMemoryStreammap")[type1.ToString()+"Adapter"];
        var assembly = Assembly.Load(ms.ToArray());
        var type = assembly.GetType(type1.ToString() + "Adapter");
        var adapterInstance = Activator.CreateInstance(type!, obj);
        return adapterInstance!;
    }

}
