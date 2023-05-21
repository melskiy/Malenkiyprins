using Hwdtech;
using System.Reflection;
namespace SpaceBattle.Lib;

public class CodeStringGenerateStrategy: IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var type1 = (Type)args[0];
        var type2 = (Type)args[1];
        var builder = IoC.Resolve<IBuilder>("GetAdapterBuilder", type1, type2);

        type2.GetProperties().ToList().ForEach(property => builder.AddProperty(property));

        return builder.Build();
    } 
}
