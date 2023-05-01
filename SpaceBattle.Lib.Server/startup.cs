using CoreWCF;
using CoreWCF.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Xml;
using WebHttp;

internal sealed class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddServiceModelWebServices(o =>
        {
            o.Title = "API для 7 лабораторной по ООАИП😞";
            o.Version = "1";
            o.Description = "Отправляем команды для работы легендарной SpaceBattlelib🔝";
            o.ContactName = "Contact";
            o.ContactUrl = new("https://vk.com/smalitskiy");
        });

        services.AddSingleton(new SwaggerOptions());
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseMiddleware<SwaggerMiddleware>();
        app.UseSwaggerUI();

        app.UseServiceModel(builder =>
        {

            builder.AddService<WebApi>();
            builder.AddServiceWebEndpoint<WebApi, IWebApi>(new WebHttpBinding
            {
                MaxReceivedMessageSize = 5242880,
                MaxBufferSize = 65536,

            }, "api", behavior =>
            {
                behavior.HelpEnabled = true;
                behavior.AutomaticFormatSelectionEnabled = true;
            });

        });
    }
}
