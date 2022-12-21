using Hwdtech;
namespace SpaceBattle.Lib;

public class PrepareData : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        List<int> List = new List<int>();
        List<int> property1 = (List<int>)args[0];
        List<int> property2 = (List<int>)args[1];
        property1.ForEach(i => List.Add(i - property2[property1.IndexOf(i)]));
        return List;
    }
}
