namespace SpaceBattle.Lib;
using Hwdtech;

public class CodeStringGenerateAdapterStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var str = (string)args[0];
        var type1 = (Type)args[1];
        var obj = (IUObject)args[2];

        return new CodeStringGenerateAdapterCommand(str,type1,obj);
    }
}
