namespace SpaceBattle.Lib;
using Hwdtech;

public class StartServer : ICommand
{
    private int Length;
    public StartServer(int Length)
    {
        this.Length = Length;
    }
    public void Execute()
    {
        for (int i = 0; i < Length; i++)
        {
            IoC.Resolve<ICommand>("CreateAndStartThreadStrategy", i).Execute();
        }
        Console.WriteLine("Запущенно " + Length + " потоков");
    }
}