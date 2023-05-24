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
        var enemy3 = new Mock<IUObject>();
        enemy1.Setup(o => o.setProperty("fuel", 5)).Verifiable();
        enemy2.Setup(o => o.setProperty("fuel", 3)).Verifiable();

        var StrategyReturnsDict = new Dictionary<string, IUObject>(){
        {"id3", enemy1.Object},
        {"id4", enemy2.Object},
        {"id5", enemy3.Object}
        };

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetFuels", (object[] args) => new List<int>() { 5, 3 }).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetFuelIndex", (object[] args) => (object)0).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUObjects", (object[] args) => StrategyReturnsDict).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameUObjectSetPropertyStrategy", (object[] args) => new GameUObjectSetPropertyStrategy().DoAlgorithm(args)).Execute();
        var Enumer = new FuelEnumerator();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "FuelEnumerator", (object[] args) => Enumer).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SetFuel", (object[] args) => new SetFuelStrategy().DoAlgorithm(args)).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SetFuelIndex", (object[] args) => new Mock<ICommand>().Object).Execute();

        var cmd = new SetupFuelCommand(new List<string>() { "id3", "id4" });

        cmd.Execute();

        enemy1.VerifyAll();
        enemy2.VerifyAll();
        Enumer.Dispose();
    }
}
