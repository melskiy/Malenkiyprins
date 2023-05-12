using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class SetupFuelCommandTests
{

    [Fact]
    public void sucsefullSetupFuelCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "DistanceBetwinShips", (object[] args) => (object)5).Execute();

        var enemy1 = new Mock<IUObject>();
        var enemy2 = new Mock<IUObject>();
        enemy1.Setup(o => o.setProperty("fuel",  5)).Verifiable();
        enemy2.Setup(o => o.setProperty("fuel",  3 )).Verifiable();

        var StrategyReturnsDict = new Dictionary<string, IUObject>(){
        {"id3", enemy1.Object},
        {"id4", enemy2.Object}
        };

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUObjects", (object[] args) => StrategyReturnsDict).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameUObjectSetPropertyStrategy", (object[] args) => new GameUObjectSetPropertyStrategy().DoAlgorithm(args)).Execute();
        var cmd = new SetupFuelCommand(new List<string>() { "id3", "id4" }, new List<int>() { 5, 3 });

        cmd.Execute();

        enemy1.VerifyAll();
        enemy2.VerifyAll();
    }
}
