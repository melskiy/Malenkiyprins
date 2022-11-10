namespace SpaceBattle.Lib;

public interface IQueue<T>
{
    public void Push(T q);
    public T Pop();
}
