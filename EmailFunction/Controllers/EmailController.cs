namespace EmailFunction.Controllers
{
    using EmailFunction.Core.DTO;
    using EmailFunction.Core.Entities;
    using EmailFunction.Core.Exceptions;
    using EmailFunction.Core.Interfaces;
    using MediatR;
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

        public async Task<EmailResponseDTO> Execute(EmailRequestDTO requestDTO)
        {

            if (string.IsNullOrWhiteSpace(requestDTO.To) || string.IsNullOrWhiteSpace(requestDTO.Subject) || string.IsNullOrWhiteSpace(requestDTO.PlainTextContent))
            {
                throw new BadRequestException();
            }

            var emailRequest = MapToEmailRequest(requestDTO);

            var response = await _mediatr.Send(emailRequest);

            var responseDTO = new EmailResponseDTO
            {
                Response = response
            };

            return responseDTO;
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
