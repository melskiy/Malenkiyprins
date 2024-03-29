namespace SpaceBattle.Lib;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.IO;
using System.Reflection;
using Hwdtech;
public class CompileStringstrategy : IStrategy
{
    public object DoAlgorithm(params object[] args)
    {
        string str = (string)args[0];
        var obj = (IUObject)args[1];
        var types = (Type)args[2];
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(str);
        Compilation compilation = CSharpCompilation.Create(types.ToString() + "Adapter")
        .AddReferences(IoC.Resolve<MetadataReference[]>("CompileReferences"));
        compilation = compilation.WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
        .AddSyntaxTrees(syntaxTree);

        using (var ms = new MemoryStream())
        {
            var type1 = types;
            var result = compilation.Emit(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }
    }
}
