namespace EmailFunction
{
    using AzureFunctions.Autofac;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    [DependencyInjectionConfig(typeof(DIConfig))]
    public static class Email
    {
        [FunctionName("Email")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [Inject]EmailController controller,
            ILogger log)
        {
            return await controller.Execute(req);
        }
    }
}
