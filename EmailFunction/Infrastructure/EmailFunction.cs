namespace EmailFunction.Infrastructure
{
    using global::EmailFunction.Controllers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;
    using Willezone.Azure.WebJobs.Extensions.DependencyInjection;

    public class EmailFunction
    {
        private EmailController _controller;
 
        public EmailFunction(EmailController controller)
        {
            _controller = controller;
        }

        [FunctionName("Email")]
        public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        ILogger log)
        {
            return await _controller.Execute(req);
        }
    }
}
