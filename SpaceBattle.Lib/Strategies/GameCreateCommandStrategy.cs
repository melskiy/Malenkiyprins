using Hwdtech;
namespace SpaceBattle.Lib;

public class GameCreateCommandStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        var message = (IMessage)args[0];

        var orderType = message.OrderType;
        var gameItemID = message.GameItemID;
        var props = message.Properties;

        var obj = IoC.Resolve<IUObject>("GetUObjectFromObjectMap", gameItemID);

        props.ToList().ForEach(p => IoC.Resolve<ICommand>("GameUObjectSetProperty", obj, p.Key, p.Value).Execute());

        return IoC.Resolve<ICommand>("CreateCommand." + orderType, obj);
    }
}
