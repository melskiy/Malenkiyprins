namespace SpaceBattle.Lib;
using Hwdtech;
public class DeleteUObjectFromObjectMapCommand : ICommand
{
    private string id;
    public DeleteUObjectFromObjectMapCommand(string id)
    {
        this.id = id;
    }

    public void Execute()
    {
       var map =  IoC.Resolve<IDictionary<string, IUObject>>("GetUObjects");
       map.Remove(id);
    }
}
