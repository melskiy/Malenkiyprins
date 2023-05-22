namespace SpaceBattle.Lib;

public class GetAdapterBuilderStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var type1 = (Type)args[0];
        var type2 = (Type)args[1];
        return new AdapterBuilder(type1, type2);
    }
}
