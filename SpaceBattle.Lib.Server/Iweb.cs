using System.Net;
using CoreWCF;
using CoreWCF.OpenApi.Attributes;
using CoreWCF.Web;

namespace WebHttp
{
    [ServiceContract]
    [OpenApiBasePath("/api")]
    internal interface IWebApi
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/command")]
        [OpenApiTag("Tag")]
        [OpenApiResponse(ContentTypes = new[] { "application/json"}, Description = "Success", StatusCode = HttpStatusCode.OK, Type = typeof(GameContract)) ]
        string HttpCommand(
            [OpenApiParameter(ContentTypes = new[] { "application/json"}, Description = "param description.")] GameContract param);
    }
}
