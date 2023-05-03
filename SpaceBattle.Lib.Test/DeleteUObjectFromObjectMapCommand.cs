namespace SpaceBattle.Lib.Test;
using Hwdtech;
using Hwdtech.Ioc;
public class DeleteUObjectFromObjectMapCommandTest
{
    [Fact]
    public void SuccessfulDeleteUObjectFromObjectMapCommandTest()
    {
        Dictionary<string, IUObject> objMap = new Dictionary<string, IUObject>(){
        {"1", new Mock<IUObject>().Object}
        };

        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUObjects", (object[] args) => objMap).Execute();

        var id = "1";

        var strategy = new GetUObjectFromObjectMapStrategy();

        var q = strategy.DoAlgorithm(id);

        Assert.Equal(objMap[id], q);

        var clean = new DeleteUObjectFromObjectMapStrategy();
        var cmd = (Lib.ICommand)clean.DoAlgorithm(id);
        cmd.Execute();

        Assert.Empty(objMap);
    }
}
