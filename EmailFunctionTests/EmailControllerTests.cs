namespace EmailFunctionTests
{
    using EmailFunction;
    using EmailFunction.Commands;
    using EmailFunction.Models;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System;
    using System.Threading;

    [TestClass]
    public class EmailControllerTests
    {
        [TestMethod]
        public void Execute_CreatesSendsEmailCommandWithEmailRequest()
        {
            var emailRequest = new EmailRequest()
            {
                To = "Robert",
                Body = "Body",
                Subject = "Subject",
                PlainTextContent = "Plain Text",
                HtmlContent = "<h1>Content</h1>"
            };

            var converterMock = new Mock<IRequestConverter>();
            Mock<IMediator> mediatorMock = new Mock<IMediator>();

            var expected = new SendEmailRequest
            {
                To = emailRequest.To,
                Body = emailRequest.Body,
                Subject = emailRequest.Subject,
                PlainTextContent = emailRequest.PlainTextContent,
                HtmlContent = emailRequest.HtmlContent
            };

            Func<SendEmailRequest, bool> matchesExpected = request => request.To == expected.To &&
                                                                        request.Body == expected.Body &&
                                                                        request.Subject == expected.Subject &&
                                                                        request.PlainTextContent == expected.PlainTextContent &&
                                                                        request.HtmlContent == expected.HtmlContent;

            converterMock.Setup(c => c.Convert(It.IsAny<HttpRequest>())).ReturnsAsync(emailRequest);
            mediatorMock.Setup(m => m.Send(It.IsAny<SendEmailRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync("Yay").Verifiable();

            var sut = new EmailController(converterMock.Object, mediatorMock.Object);
            sut.Execute(It.IsAny<HttpRequest>());

            mediatorMock.Verify(m => m.Send(It.Is<SendEmailRequest>(r => matchesExpected(r)), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
