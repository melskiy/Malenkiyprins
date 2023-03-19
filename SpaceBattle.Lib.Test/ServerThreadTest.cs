using Hwdtech;
using Hwdtech.Ioc;
using System.Collections.Concurrent;
using Moq;
using Xunit.Abstractions;

namespace SpaceBattle.Lib.Test;

public class ServerThreadTests
{
    Mock<ICommand> emptyCmd = new Mock<ICommand>();
    ConcurrentDictionary<int, ServerThread> mapServerThreads = new ConcurrentDictionary<int, ServerThread>();
    ConcurrentDictionary<int, ISender> mapServerThreadsSenders = new ConcurrentDictionary<int, ISender>();

    private readonly ITestOutputHelper output;

    public ServerThreadTests(ITestOutputHelper output)
    {
        this.output = output;
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "ThreadMap", (object[] args) => mapServerThreads).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SenderMap", (object[] args) => mapServerThreadsSenders).Execute();

        emptyCmd.Setup(c => c.Execute()).Callback(() => {});
    }

    [Fact]
    public void SuccessfulCreateStartAndHardStopServerThreadWithoutParamsForStrategies()
    {
        var isActive = false;

        var key = 1;
        
        var mre = new ManualResetEvent(false);

        IStrategy createAndStartSTStrategy = new CreateAndStartThreadStrategy();
        var c = (ICommand)createAndStartSTStrategy.DoAlgorithm(key);
        c.Execute();

        var sendStrategy = new SendCommandStrategy();

        var c1 = (ICommand)sendStrategy.DoAlgorithm(key, new ActionCommand(() =>
        {
            isActive = true;
            mre.Set();
        }));

        c1.Execute();

        mre.WaitOne();

        Assert.True(isActive);
        Assert.True(mapServerThreads.TryGetValue(key, out ServerThread? st));
        Assert.True(mapServerThreadsSenders.TryGetValue(key, out ISender? s));

        var hardStopStrategy = new HardStopServerThreadCommandStrategy();

        var hs = (ICommand)hardStopStrategy.DoAlgorithm(key);

        hs.Execute();       
    }

    [Fact]
    public void SuccessfulCreateStartAndHardStopServerThreadWithParamsForStrategies()
    {
        var isActive = false;
        var createAndStartFlag = false;
        var hsFlag = false;

        var key = 1;
        
        var mre = new ManualResetEvent(false);

        IStrategy createAndStartSTStrategy = new CreateAndStartThreadStrategy();
        var c = (ICommand)createAndStartSTStrategy.DoAlgorithm(key, () => {
            createAndStartFlag = true;
        });
        c.Execute();

        var sendStrategy = new SendCommandStrategy();

        var c1 = (ICommand)sendStrategy.DoAlgorithm(key, new ActionCommand(() =>
        {
            isActive = true;
            mre.Set();
        }));

        c1.Execute();

        mre.WaitOne();

        Assert.True(isActive);
        Assert.True(mapServerThreads.TryGetValue(key, out ServerThread? st));
        Assert.True(mapServerThreadsSenders.TryGetValue(key, out ISender? s));
        Assert.True(createAndStartFlag);


        var hardStopStrategy = new HardStopServerThreadCommandStrategy();

        var hs = (ICommand)hardStopStrategy.DoAlgorithm(key, () => {
            hsFlag = true;
            mre.WaitOne();
        });

        hs.Execute();   

        output.WriteLine($"{hsFlag}");

        Assert.True(hsFlag);
    }
}
