namespace EmailFunction
{
    using EmailFunction.Commands;
    using EmailFunction.Models;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class EmailController
    {
        private EmailRequestConverter emailRequestConverter { get; set; }
        private IMediator _mediatr { get; set; }

        public EmailController(EmailRequestConverter converter, IMediator mediatr)
        {
            emailRequestConverter = converter;
            _mediatr = mediatr;
        }

        public async Task<ActionResult> Execute(HttpRequest req)
        {
            var emailRequest = await emailRequestConverter.Convert(req);

            var response = await _mediatr.Send(new SendEmailRequest
            {
                To = emailRequest.To,
                Body = emailRequest.Body
            });

            return (ActionResult)new OkObjectResult($"Hello");
        }
    }
}
