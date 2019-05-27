namespace EmailFunction.Infrastructure
{
    using global::EmailFunction.Controllers;
    using global::EmailFunction.Core.DTO;
    using global::EmailFunction.Core.Exceptions;
    using global::EmailFunction.Core.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    public class EmailFunction
    {
        private EmailController _controller;

        private IRequestDeserializer _requestDeserializer;

        public EmailFunction(EmailController controller, IRequestDeserializer requestDeserializer)
        {
            _controller = controller;
            _requestDeserializer = requestDeserializer;
        }

        [FunctionName("Email")]
        public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
        ILogger log)
        {
            var requestDTO = await _requestDeserializer.Deserialize<EmailRequestDTO>(req);

            try
            {
                var response = await _controller.Execute(requestDTO);

                return new OkObjectResult(response);
            } catch (BadRequestException)
            {
                return new BadRequestObjectResult("Bad email request");
            }

        }
    }
}
