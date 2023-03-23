namespace SpaceBattle.Lib;
using Hwdtech;
class Program
{
    static void Main(string[] args)
    {
        int numThreads = int.Parse(args[0]);
        app = new AppRun(numThreads);
        AppRun.Execute();
    }

}
