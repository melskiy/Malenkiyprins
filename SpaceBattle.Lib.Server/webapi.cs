using CoreWCF;
using Hwdtech;

namespace WebHttp;

[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
internal class WebApi : IWebApi
{
    public string HttpCommand(GameContract param)
    {
        IoC.Resolve<ICommand>("HandleCommandStrategy", param).Execute();
        return "ok";
    }
}
