namespace SpaceBattle.Lib;
using Hwdtech;
public class CodeStringGenerateAdapterCommand : ICommand
{
    private string str;
    private Type type1;
    private IUObject obj;
    public CodeStringGenerateAdapterCommand(string str, Type type1, IUObject obj)
    {
        this.str = str;
        this.type1 = type1;
        this.obj = obj;
    }
    public void Execute()
    {
        var ms = IoC.Resolve<MemoryStream>("CompileStringstrategy", str, obj,type1);
        var adapterName = type1.ToString() + "Adapter";
        var map = IoC.Resolve<IDictionary<string,MemoryStream>>("GetMemoryStreammap");
        map.Add(type1.ToString()+"Adapter",ms);
        var dic = IoC.Resolve<IDictionary<KeyValuePair<Type, Type>, string>>("CodeStringmap");
        dic.Add(new KeyValuePair<Type, Type>(obj.GetType(), type1), adapterName);
    }
}

