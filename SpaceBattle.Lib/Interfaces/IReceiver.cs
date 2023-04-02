namespace SpaceBattle.Lib;

public interface IReceiver
{
    public ICommand Receive();
    public bool isEmpty();
}
