namespace SpaceBattle.Lib.Test;
public class AddTwoNumbersStrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        int a = (int)args[0];
        int b = (int)args[1];
        return a + b;
    }
}


public class IoCTest
{
    [Fact]
    public void IoCTest1()
    {
        IoC.Resolve<ICommand>("IoC.Add", "Game.OfTwo", new AddTwoNumbersStrategy()).Execute();
        var res = IoC.Resolve<int>("Game.OfTwo", 1, 2);
        AssemblyLoadEventArgs.Equals(3, res);
    }
}
