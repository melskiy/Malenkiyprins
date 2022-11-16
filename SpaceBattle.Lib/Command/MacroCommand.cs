namespace SpaceBattle.Lib;
public class MacroCommand : ICommand
{
    IEnumerable<ICommand> arr;
    public MacroCommand(params ICommand[] arr)
    {
        this.arr = arr;
    }
    public MacroCommand(IEnumerable<ICommand> arr)
    {
        this.arr = arr;
    }
    public void Execute() { arr.ToList().ForEach(id => id.Execute()); }
}
