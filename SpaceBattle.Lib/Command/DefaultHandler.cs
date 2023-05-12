namespace SpaceBattle.Lib;

public class DefaultHandler : ICommand
{
    private Exception _exception;
    private ICommand _cmd;
    public DefaultHandler(Exception exception, ICommand cmd)
    {
        _exception = exception;
        _cmd = cmd;
    }
    public void Execute()
    {
        _exception.Data.Add("Command", _cmd);
        throw _exception;
    }
}
