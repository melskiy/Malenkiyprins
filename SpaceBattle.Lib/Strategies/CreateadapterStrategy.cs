namespace SpaceBattle.Lib;
using Hwdtech;
public class CreateadapterStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var obj = (IUObject)args[0];
        var type2 = (Type)args[1];
        var str = IoC.Resolve<string>("CodeStringGenerateStrategy", obj, type2);
        var dic = IoC.Resolve<IDictionary<KeyValuePair<Type, Type>, string>>("CodeStringmap");
        var key = new KeyValuePair<Type, Type>(obj.GetType(), type2);
        if (!dic.TryGetValue(key, out var adapterName))
        {
            IoC.Resolve<ICommand>("CodeStringGenerateAdapter", str, type2, obj).Execute();
        }
        var adapter = IoC.Resolve<object>("FindAdapterStrategy",type2,obj);
        return adapter;
    }

}
