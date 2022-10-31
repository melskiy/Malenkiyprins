namespace SpaceBattle.Lib;
public class MacroCommand : ICommand {
    List<ICommand> arr = new();
    MacroCommand (List<ICommand> arr){
        this.arr = arr;
    }
   public void Execute(){arr.ForEach(cmd => cmd.Execute());}
}
