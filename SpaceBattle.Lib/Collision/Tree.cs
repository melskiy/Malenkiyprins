using Hwdtech;
using System.Text;
namespace SpaceBattle.Lib;



public class TreeCreate:ICommand
{
    string path;
    public TreeCreate(string path){
        this.path = path;
    }
    public void Execute()
    {
        var den = IoC.Resolve<IDictionary<int, object>>("ICollisionTreeRootDictionary");
  
        var g =  System.IO.File.ReadAllLines(path).Select(x => x.Split().Select(int.Parse)).ToList();
        g.ForEach(
            line => {
                var den2 = den;
                line.ToList().ForEach(
                    i => {
                        den2.TryAdd(i, new Dictionary<int, object>());
                        den2 = (IDictionary<int, object>)den2[i];
                    }
                );
            }
        ); 
    }
}


