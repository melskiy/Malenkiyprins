using Hwdtech;
namespace SpaceBattle.Lib;

public class CreateEmptyGameObjectsStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var num = IoC.Resolve<int>("GetNumObjects");
        var map = Enumerable.Range(1, num)
                    .ToDictionary(i => "id" + i.ToString(), i => IoC.Resolve<IUObject>("GetUobjectWithStartParams"));

        return map;
    }
}
