namespace SpaceBattle.Lib;
using System.Diagnostics.CodeAnalysis;

public class GenerateAdapter : IStrategy
{

    public object DoAlgorithm(params object[] args)
    {
        Type a = (Type)args[0];
        IUObject obj = (IUObject)args[1];

        if (Activator.CreateInstance(a, obj) is null)
        {
            throw new ArgumentNullException();
        }
        else
        {
            return Activator.CreateInstance(a, obj);
        }

    }

}