namespace SpaceBattle.Lib;
using Hwdtech;

public class AppRun : ICommand
{
    private int numthreads;
    public AppRun(int numthreads){
        this.numthreads = numthreads;
    }
    public void Execute()
    {
        
        Console.WriteLine("Нажмите на любую клавишу для запуска....");
        Console.Read();

        IoC.Resolve<ICommand>("StartServerStrategy", numthreads).Execute();

        Console.WriteLine("Нажмите на любую клавишу для завершения работы сервера....");

        Console.Read();

        IoC.Resolve<ICommand>("StopServerStrategy").Execute();
        Console.WriteLine("Все потоки успешно остановеленны 😍...");

    }
}
