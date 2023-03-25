namespace SpaceBattle.Lib;
using Hwdtech;
using System.Collections.Concurrent;

public class HandlerWrFileCommand : ICommand
{
    private IEnumerable<Type> listoftypes;
    private string path;
    public HandlerWrFileCommand(IEnumerable<Type> listoftypes, string path = "")
    {
        this.listoftypes = listoftypes;
        this.path = path;
    }
    public void Execute()
    {
        using (StreamWriter sw = File.AppendText($"{path}log.txt"))
        {
            ((IEnumerable<Type>)listoftypes).ToList().ForEach(item => sw.WriteLine(item));
        }
    }
}
