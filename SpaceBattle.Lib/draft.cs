// namespace SpaceBattle.Lib;
// public interface IStrategy
// {
//     Object operator(args);
// }
// public class StartMoveCommand
// {
//     //конструктор...
//     void Execute()
//     {
//         ICommand rmc = IoC<ICommand>("Game.Commands.RepeatMoveCommand", q);
//         IOC<ICommand>("UObject.SetProperty", obj, "MoveOperation", rmc);
//         q.Push(rmc);
//     }
// };

// public interface IQueue{
//     void Push(T);
//     T Pop();
// }
// public class StopMoveCommand: ICommand{
//     StopMoveCommand(IUobject obj)
//     {
        
//     }
//     void Execute()
//     {
//         obj.setProperty("MoveOperation", new EmptyCommand());
//     }
// }
// public class RepeatCommand
// {
//     RepeatCommand(ICommand c, IQueue q) {}

//     execute() 
//     {
//         c.execute();
//         q.push(this);
//     }
// }
// public class IOC
// {
//     static Dictionary<string, IStrategy> store;

//     static Resolve<T>(string key, args)
//     {
//         return(T) store["IoC.Resolve"](args);
//     }
// };

// public interface IModule
// {
//     ICommand loadCommand
//     {
//         get;
//     }
// }