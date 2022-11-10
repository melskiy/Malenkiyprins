namespace SpaceBattle.Lib;
//Activator
//Imovable movable =IoC.Resolve<IMovable>("GenerateAdapter", typeOf(IMovable),object);  стратегия 
//System refllection
//Написать стратегию для генерации адаптеров
// Тред локал айм трап айм трап вылизал её с головы до ног оу оу я снова обогнал, снова обогнал предлагали 10, забрал 10 по 10
// public class MovableAdapter: IMovable
// {
//     Vector Position
//     {
//         get{
//             return (Vector)IoC.Resolve<object>("UObject.getProperty", this.obj, "Position");
//         }
//         set (object value){
//             IoC.Resolve<ICommand>("UObject.setProperty", this.obj, "Position");
//         }
//     }
//     Vector Velocity
//     {
//         get{
//             return (Vector)IoC.Resolve<object>("UObject.getProperty", this.obj, "Velocity");
//         }
//     }
// }