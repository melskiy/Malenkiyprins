using Hwdtech;
namespace SpaceBattle.Lib;

public class RuleInicializationGameCommand : IStrategy
{
    IStrategy createOperationStrategy = new CreateOperationStrategy();

    public object DoAlgorithm(params object[] args)
    {
        return new ActionCommand(() => {
            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Operations.Create", (object[] args) => this.createOperationStrategy.DoAlgorithm(args)).Execute();
        });
    }
}
