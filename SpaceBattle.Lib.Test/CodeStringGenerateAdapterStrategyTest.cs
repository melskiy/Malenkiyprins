using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;
using Microsoft.CodeAnalysis;
public class CodeStringGenerateAdapterStrategyTest
{
    [Fact]
    public void SuccsisfulGenerateCodeStringTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        var mockUObject = new Mock<IUObject>();

        string _template_text = @"namespace SpaceBattle.Lib{
public class IMovableAdapter : IMovable
{
    private IUObject obj;
    public IMovableAdapter(IUObject obj) => this.obj = obj;
    public Vector Position
    {
        get => new Vector(new int[] { 0, 1 });
        set => new Vector(new int[] { 0, 1 });
    }
    public Vector Velocity
    {
        get => new Vector(new int[] { 0, 1 });
    }
}
}";
         MetadataReference[] references = new MetadataReference[]
         {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile("../../../../SpaceBattle.Lib/bin/Debug/net6.0/SpaceBattle.Lib.dll")
         };
        var dict = new Dictionary<KeyValuePair<Type, Type>, string>();
        var dict2 = new Dictionary<string, MemoryStream>();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CompileReferences", (object[] args) => (object)references).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CodeStringmap", (object[] args) => dict).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CompileStringstrategy", (object[] args) => new CompileStringstrategy().DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CodeStringGenerateStrategy", (object[] args) => (object)_template_text).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CodeStringGenerateAdapter", (object[] args) => new CodeStringGenerateAdapterStrategy().DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "FindAdapterStrategy", (object[] args) => new FindAdapterStrategy().DoAlgorithm(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetMemoryStreammap", (object[] args) => (object)dict2).Execute();
        var srtategy = new CreateadapterStrategy();
        var result = (IMovable)srtategy.DoAlgorithm(mockUObject.Object, typeof(IMovable));
        var code = result.ToString();
        Assert.Equal("SpaceBattle.Lib.IMovableAdapter",code);
        Assert.Equal(new Vector(new int[] { 0, 1 }),result.Velocity);
    }
}
