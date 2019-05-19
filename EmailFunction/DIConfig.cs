namespace EmailFunction
{
    using Autofac;
    using AzureFunctions.Autofac.Configuration;
    using EmailFunction.Commands;
    using EmailFunction.Models;
    using MediatR;
    using System.Reflection;
    using EmailFunction.Infrastructure;

    public class DIConfig
    {
        public DIConfig(string functionName)
        {
            DependencyInjection.Initialize(builder =>
            {
                builder.RegisterType<EmailController>();
                builder.RegisterType<EmailRequestConverter>().As<IRequestConverter>();
                builder.RegisterType<SendGrid>().As<IMessageProcessor>();

                builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

                var mediatrOpenTypes = new[]
                     {
                        typeof(IRequestHandler<,>),
                        typeof(INotificationHandler<>),
                    };

                foreach (var mediatrOpenType in mediatrOpenTypes)
                {
                    builder
                        .RegisterAssemblyTypes(typeof(SendEmailRequest).GetTypeInfo().Assembly)
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
