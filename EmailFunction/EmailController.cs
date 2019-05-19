namespace EmailFunction
{
    using EmailFunction.Commands;
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
            var emailRequest = await converter.Convert(req);

            var response = await _mediatr.Send(new SendEmailRequest
            {
                To = emailRequest.To,
                Body = emailRequest.Body,
                Subject = emailRequest.Subject,
                PlainTextContent = emailRequest.PlainTextContent,
                HtmlContent = emailRequest.HtmlContent
            });

            return (ActionResult)new OkObjectResult($"Hello");
        }
    }
}
