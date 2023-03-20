
namespace SpaceBattle.Lib;
using System;
using System.Collections.Generic;
using System.Threading;
using Hwdtech;
using System.Diagnostics.CodeAnalysis;
[ExcludeFromCodeCoverage]
class Program
{
    static void Main(string[] args)
    {
        int numThreads = int.Parse(args[0]);

        Console.WriteLine("Нажмите на любую клавишу для запуска....");
        Console.ReadKey();

        IoC.Resolve<ICommand>("StartServerStrategy",numThreads);

        Console.WriteLine("Нажмите на любую клавишу для завершения работы сервера....");

        Console.ReadKey();
        
        IoC.Resolve<ICommand>("StopServerStrategy");
        Console.WriteLine("Все потоки успешно остановеленны 😍...");
       
    }

}