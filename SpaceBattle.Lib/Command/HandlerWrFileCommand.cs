namespace SpaceBattle.Lib;
using Hwdtech;
using System.Collections.Concurrent;

public class HandlerWrFileCommand : ICommand
{
    private IEnumerable<Type> listoftypes;
    public HandlerWrFileCommand(IEnumerable<Type> listoftypes)
    {
        this.listoftypes = listoftypes;
    }
    public void Execute()
    {
        using (StreamWriter sw = File.AppendText("log.txt"))
        {
            ((IEnumerable<Type>)listoftypes).ToList().ForEach(item => sw.WriteLine(item));
        }
    }
}
