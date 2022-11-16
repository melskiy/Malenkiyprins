namespace SpaceBattle.Lib;


public class QueuePushStrategy<T> : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        IQueue<T> q = (IQueue<T>)args[0];
        T par = (T)args[1];
        return new QueuePushCommand<T>(q, par);
    }
}

public class QueuePushCommand<T> : ICommand
{
    private IQueue<T> q;
    private T target;
    public QueuePushCommand(IQueue<T> q, T target)
    {
        this.q = q;
        this.target = target;
    }
    public void Execute()
    {
        q.Push(target);
    }
}

public class GetQueueStrategy<T> : IStrategy
{
    IQueue<T> q;
    public GetQueueStrategy(IQueue<T> q)
    {
        this.q = q;
    }
    public object DoAlgorithm(params object[] args)
    {
        return this.q;
    }
}

// IoC.Resolve<ICommand>("Ioc.Add", "Game.Queue", new GetQueueStrategy(IQueue<ICommand>)).Execute() for test
