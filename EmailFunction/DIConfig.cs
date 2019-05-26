namespace EmailFunction
{
    using Autofac;
    using AzureFunctions.Autofac.Configuration;
    using EmailFunction.Controllers;
    using EmailFunction.Core.Entities;
    using EmailFunction.Core.Interfaces;
    using EmailFunction.Infrastructure;
    using MediatR;
    using System.Reflection;

    public class DIConfig
    {
        public DIConfig(string functionName)
        {
            DependencyInjection.Initialize(builder =>
            {
                builder.RegisterType<EmailController>();
                builder.RegisterType<RequestDeserializer>().As<IRequestDeserializer>();
                builder.RegisterType<SendGridMessageProcessor>().As<IMessageProcessor>();
                builder.RegisterType<Configuration>().As<IConfiguration>();

                builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

                var mediatrOpenTypes = new[]
                     {
                        typeof(IRequestHandler<,>),
                        typeof(INotificationHandler<>),
                    };

                foreach (var mediatrOpenType in mediatrOpenTypes)
                {
                    builder
                        .RegisterAssemblyTypes(typeof(EmailRequest).GetTypeInfo().Assembly)
                        .AsClosedTypesOf(mediatrOpenType)
                        .AsImplementedInterfaces();
                }

                builder.Register<ServiceFactory>(ctx =>
                {
                    var c = ctx.Resolve<IComponentContext>();
                    return t => c.Resolve(t);
                });


            }, functionName);
        }
    }
}
