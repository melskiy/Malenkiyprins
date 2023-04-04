using Hwdtech;
using Hwdtech.Ioc;


namespace SpaceBattle.Lib.Test;

public class GetUObjectFromObjectMapStrategyTest
{
    Dictionary<string, IUObject> objMap = new Dictionary<string, IUObject>(){
        {"1", new Mock<IUObject>().Object}
    };

    public GetUObjectFromObjectMapStrategyTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUObjects", (object[] args) => this.objMap).Execute();
    }

    [Fact]
    public void SuccessfulGetUObjectFromObjectMapStrategy()
    {
        var id = "1";

        var strategy = new GetUObjectFromObjectMapStrategy();

        var q = strategy.DoAlgorithm(id);

        Assert.Equal(this.objMap[id], q);
    }

    [Fact]
    public void UnsuccessfulGetUObjectFromObjectMapStrategy()
    {
        var falseID = "2";

        var strategy = new GetUObjectFromObjectMapStrategy();

        Assert.Throws<Exception>(() => strategy.DoAlgorithm(falseID));
    }
}
