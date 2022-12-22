using Hwdtech;
namespace SpaceBattle.Lib;

public class FindHandler : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var c = (Type)args[0];
        var exception = (Type)args[1];
        var HandlerTree = IoC.Resolve<IDictionary<object, IDictionary<object, IHandler>>>("ExeptionTree");
        var Tree2 = HandlerTree[c];
        if (!(Tree2.TryGetValue(exception, out IHandler? value)))
        {
            return Tree2["default"];
        }
        return value;
    }
}
