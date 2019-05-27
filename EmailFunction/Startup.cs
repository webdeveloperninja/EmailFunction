using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(EmailFunction.Startup))]
namespace EmailFunction
{
    using EmailFunction.Controllers;
    using EmailFunction.Core.Interfaces;
    using EmailFunction.Infrastructure;
    using MediatR;
    using Microsoft.Azure.Functions.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using System.Reflection;

    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<EmailController>();
            builder.Services.AddTransient<IConfiguration, Configuration>();
            builder.Services.AddTransient<IMessageProcessor, SendGridMessageProcessor>();
            builder.Services.AddTransient<IRequestDeserializer, RequestDeserializer>();
            builder.Services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            builder.Services.AddLogging();
        }
    }
}
