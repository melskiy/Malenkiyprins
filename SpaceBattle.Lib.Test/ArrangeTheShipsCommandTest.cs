using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class ArrangeTheShipsCommandTest
{

    [Fact]
    public void SucsefullArrangeTheShipsCommandTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "DistanceBetwinShips", (object[] args) => (object)5).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetCurrentShip", (object[] args) => (object)0).Execute();
        var ship = new Mock<IUObject>();
        var ship2 = new Mock<IUObject>();
        ship2.Setup(o => o.getProperty("position")).Returns(new Vector(new int[]{0,1}));
        ship.Setup(o => o.getProperty("position")).Returns(new Vector(new int[]{0,1}));
        
        var enemy1 = new Mock<IUObject>();
        var enemy2 = new Mock<IUObject>();
        enemy1.Setup(o => o.setProperty("position", new Vector(new int[]{5,1}))).Verifiable();
        enemy2.Setup(o => o.setProperty("position", new Vector(new int[]{5,1}))).Verifiable();

        var StrategyReturnsDict = new Dictionary<string, IUObject>(){
        {"id3", enemy1.Object},
        {"id4", enemy2.Object}
        };

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUObjects", (object[] args) => StrategyReturnsDict).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetShipPositions", (object[] args) => new List<Vector>(){new Vector(new int[]{0,1}),new Vector(new int[]{0,1})}).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameUObjectSetPropertyStrategy", (object[] args) => new GameUObjectSetPropertyStrategy().DoAlgorithm(args)).Execute();
        var cmd = new ArrangeTheShipsCommand( new List<string>() { "id3", "id4" });

        cmd.Execute();
        
        enemy1.VerifyAll();
        enemy2.VerifyAll();
    }

}
