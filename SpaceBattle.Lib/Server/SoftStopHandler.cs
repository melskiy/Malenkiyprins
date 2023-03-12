namespace SpaceBattle.Lib;
using Hwdtech;
using System.Collections.Concurrent;

public class SoftStopHandler : ICommand
{
    private IEnumerable<Type> listoftypes;
    public SoftStopHandler(IEnumerable<Type> listoftypes){
        this.listoftypes = listoftypes;
    }
    public void Execute()
    {
       using (StreamWriter sw = File.AppendText("log.txt"))
        {
           ((IEnumerable<Type>)listoftypes).ToList().ForEach(item =>  sw.WriteLine(item));
        }
    }
}
