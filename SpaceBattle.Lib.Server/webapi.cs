using CoreWCF;
using Hwdtech;

namespace WebHttp;

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    internal class WebApi : IWebApi
    {
        public GameContract HttpCommand(GameContract param){
            IoC.Resolve<ICommand>("HttpCommandStrategy", param).Execute();
            return param;
        }   
    }
