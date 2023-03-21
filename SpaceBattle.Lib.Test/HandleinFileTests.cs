
using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

   
public class HandlerWrFileStrategyTests
    {
        [Fact]
        public void Execute_WithListContainingTypes_WritesTypesToFile()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
            IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
           

            var listoftypes = new List<Type> { typeof(string), typeof(int), typeof(DateTime) };


           var cmdeshka = new HandlerWrFileCommand(listoftypes);
        
           cmdeshka.Execute();
       

            Assert.True(File.Exists("log.txt"));

            var lines = File.ReadAllLines("log.txt");
            Assert.Equal("System.String", lines[0]);
            Assert.Equal("System.Int32", lines[1]);
            Assert.Equal("System.DateTime", lines[2]);
           
        }

        
    
    }
    