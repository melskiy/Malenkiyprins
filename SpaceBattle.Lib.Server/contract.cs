using System.Collections.Generic;
using System.Runtime.Serialization;
using CoreWCF.OpenApi.Attributes;
using SpaceBattle.Lib;
namespace WebHttp
{
    [DataContract(Name = "ExampleContract", Namespace = "http://example.com")]
    internal class GameContract:IMessage
    {
        [DataMember(Name = "OrderType")]
        [OpenApiProperty(Description = "тип команды")]
        public string OrderType {get;set;}


        [DataMember(Name = "GameID")]
        [OpenApiProperty(Description = "id игры")]
        public string GameID { get;set;}

        [DataMember(Name = "GameItemID")]
        [OpenApiProperty(Description = "id объекта")]
        public string GameItemID { get; set; }

        [DataMember(Name = "Properties")]
        [OpenApiProperty(Description = "свойстав объекта")]
        public IDictionary<string,object> Properties { get; set ;}
    }
}
