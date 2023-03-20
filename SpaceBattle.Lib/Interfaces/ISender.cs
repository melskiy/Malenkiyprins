namespace SpaceBattle.Lib;

public interface ISender
{
    public void Send(ICommand message);
}
