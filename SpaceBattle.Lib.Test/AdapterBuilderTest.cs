namespace SpaceBattle.Lib.Test;

public class AdapterBuilderTestTest
{
    [Fact]
    public void SuccessfulGetAdapterBuilderTest()
    {
        var adapterBuilder = new AdapterBuilder(typeof(IUObject), typeof(IMovable));
        typeof(IMovable).GetProperties().ToList().ForEach(property => adapterBuilder.AddProperty(property));

        var imovableAdapter = @"public class IMovableAdapter : IMovable
    {
        private IUObject obj;

        public IMovableAdapter(IUObject obj) => this.obj = obj;

        public Vector Position
        {
            get => IoC.Resolve<Vector>(""GetPosition"", obj);
            set => IoC.Resolve<ICommand>(""SetPosition"", obj, value).Execute();
        }

        public Vector Velocity
        {
            get => IoC.Resolve<Vector>(""GetVelocity"", obj);
            
        }
    }";
        Assert.True(adapterBuilder.Build() == imovableAdapter);
    }
}
