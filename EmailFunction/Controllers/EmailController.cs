namespace EmailFunction.Controllers
{
    using EmailFunction.Core.Entities;
    using EmailFunction.Core.Interfaces;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class EmailController
    {
        private IRequestDeserializer _requestDeserializer { get; set; }

        private IMediator _mediatr { get; set; }

        private IConfiguration _configuration { get; set; }

        public EmailController(IRequestDeserializer requestDeserializer, IMediator mediatr, IConfiguration configuration)
        {
            _requestDeserializer = requestDeserializer;
            _mediatr = mediatr;
            _configuration = configuration;
        }

        public async Task<ActionResult> Execute(HttpRequest req)
        {
            var requestDTO = await _requestDeserializer.Deserialize<EmailRequestDTO>(req);

            if (string.IsNullOrWhiteSpace(requestDTO.To) || string.IsNullOrWhiteSpace(requestDTO.Subject) || string.IsNullOrWhiteSpace(requestDTO.PlainTextContent))
            {
                return new BadRequestObjectResult("Email request not valid");
            }

            var emailRequest = MapToEmailRequest(requestDTO);

            var response = await _mediatr.Send(emailRequest);

            return new OkObjectResult(response);
        }

        private EmailRequest MapToEmailRequest(EmailRequestDTO requestDTO)
        {
            var emailRequest = new EmailRequest(fromAddress: _configuration.FromAddress, fromName: _configuration.FromName, toAddress: requestDTO.To);
            emailRequest.PlainTextContent = requestDTO.PlainTextContent;
            emailRequest.Subject = requestDTO.Subject;
            emailRequest.HtmlContent = requestDTO.HtmlContent;

            return emailRequest;
        }
    }
}
