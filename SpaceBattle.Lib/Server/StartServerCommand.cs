namespace SpaceBattle.Lib;
using Hwdtech;
using System.Collections.Concurrent;

public class StartServer : ICommand
{
    private int Length;
    public StartServer(int Length){
        this.Length = Length;
    }
    public void Execute()
    {
        if (Length == 0)
        {
            Console.WriteLine("Передайте число потоков");
            throw new Exception();
        }

    
        Console.WriteLine("Запущенно" + Length + "патоков");

        for (int i = 0; i < Length; i++)
        {
            IoC.Resolve<ICommand>("CreateAndStartThreadStrategy",i);
        }
    }
}
