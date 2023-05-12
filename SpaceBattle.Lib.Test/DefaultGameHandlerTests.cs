namespace SpaceBattle.Lib.Test;

public class DefaultHandlerTest
{
    [Fact]
    public void SuccessfulDefaultHandler()
    {
        var cmd = new Mock<ICommand>();
        var exception = new Exception();

        var defaultHandler = new DefaultHandler(exception, cmd.Object);

        try
        {
            defaultHandler.Execute();
        }
        catch (Exception ex)
        {
            Assert.Equal(cmd.Object, ex.Data["Command"]);
        }
    }
}
