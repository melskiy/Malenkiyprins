namespace SpaceBattle.Lib.Test;

public class GetAdapterBuilderStrategyTest
{
    [Fact]
    public void SuccessfulGetAdapterBuilderStrategy()
    {
        var getAdapterBuilderStrategy = new GetAdapterBuilderStrategy();
        var builder = (IBuilder)getAdapterBuilderStrategy.DoAlgorithm(typeof(IUObject), typeof(IMovable));
        Assert.NotNull(builder);
        Assert.True(typeof(AdapterBuilder) == builder.GetType());
    }
}
