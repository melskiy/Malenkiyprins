using Hwdtech;
using System.Text;
namespace SpaceBattle.Lib;


public class FindHandlerStrategy : IStrategy
{

    public object DoAlgorithm(params object[] args)
    {
        var types = (IEnumerable<Type>)args[0];

        var hashes = IoC.Resolve<int>("GetHashStrategy", types.OrderBy(x => x.GetHashCode()));
        var handler_tree = IoC.Resolve<IDictionary<int, ICommand>>("ExceptionTree");

        if (handler_tree.TryGetValue(hashes, out ICommand? value))
        {
            return value;
        }
        return handler_tree[0];
    }
}
