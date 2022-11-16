namespace SpaceBattle.Lib;
public class MacroCommand : ICommand
{
    IEnumerable<ICommand> arr;
    MacroCommand(params ICommand[] arr)
    {
        this.arr = arr;
    }
    public void Execute() { arr.ToList().ForEach(id => id.Execute()); }
}
