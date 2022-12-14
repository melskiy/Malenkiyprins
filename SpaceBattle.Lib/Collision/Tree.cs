using Hwdtech;
using System.Text;
namespace SpaceBattle.Lib;

public interface IDerevo{
    public void read();
}

public class TreeCreate
{
    int x;
    public void read()
    {
        var den = IoC.Resolve<IDictionary<int, object>>("ICollisionTreeRootDictionary");
  
        var g =  System.IO.File.ReadAllLines(@".\Vectors.txt").Select(x => x.Split().Select(int.Parse)).ToList();
        g.ForEach(
            line => {
                // [1, 2, 3, 4]
                // [5, 6, 7, 8]
                var den2 = den;
                line.ToList().ForEach(
                    //1
                    i => {
                        den2.TryAdd(i, new Dictionary<int, object>());
                        den2 = (IDictionary<int, object>)den2[i];
                    }
                );
            }
        ); 
    }
}


public class SSStolknovenie
{

}
