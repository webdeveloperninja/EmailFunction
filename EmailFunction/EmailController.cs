namespace EmailFunction
{
    using EmailFunction.Commands;
    using EmailFunction.Exceptions;
    using EmailFunction.Models;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class EmailController
    {
        private IRequestConverter converter { get; set; }
        private IMediator _mediatr { get; set; }

        public EmailController(IRequestConverter converter, IMediator mediatr)
        {
            this.converter = converter;
            _mediatr = mediatr;
        }

        public async Task<ActionResult> Execute(HttpRequest req)
        {
            var emailRequest = new EmailRequest();

            try
            {
                emailRequest = await emailRequest.Create(req, converter);
            } catch (RequestConverterException)
            {
                return new BadRequestObjectResult($"Could not process your email request");
            }

            var response = await _mediatr.Send(new SendEmailRequest
            {
                To = emailRequest.To,
                Subject = emailRequest.Subject,
                PlainTextContent = emailRequest.PlainTextContent,
            });

            return (ActionResult)new OkObjectResult(response);
        }
    }
}
