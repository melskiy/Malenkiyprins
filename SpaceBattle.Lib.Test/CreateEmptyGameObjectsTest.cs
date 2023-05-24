using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class CreateEmptyGameObjectsTest
{
    [Fact]
    public void SucsefullEmptyGameObjectsTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "DistanceBetwinShips", (object[] args) => (object)5).Execute();
        var obj = new Mock<IUObject>();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetNumObjects", (object[] args) => (object)2).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUobjectWithStartParams", (object[] args) => obj.Object).Execute();

        var strategy = new CreateEmptyGameObjectsStrategy();
        var Result = (Dictionary<string, IUObject>)(strategy.DoAlgorithm(10));

        Assert.True(Result["id2"].Equals(obj.Object));
        Assert.True(Result["id1"].Equals(obj.Object));
    }

}
