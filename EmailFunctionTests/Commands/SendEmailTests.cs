namespace EmailFunctionTests.Commands
{
    using EmailFunction;
    using EmailFunction.Commands;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System;
    using System.Threading;

    [TestClass]
    public class SendEmailTests
    {
        [TestMethod]
        public void Handle_ProcessesMessageWithRequest()
        {
            var messageProcessorMock = new Mock<IMessageProcessor>();
            var fromEmail = "Robert@test.com";
            Environment.SetEnvironmentVariable("From_Email", fromEmail);

            var request = new SendEmailRequest
            {
                To = "Robert@test.com",
                Body = "Body",
                Subject = "Subject",
                PlainTextContent = "Plain Text",
                HtmlContent = "<h1>Content</h1"
            };

            var expected = new MessageRequest
            {
                To = request.To,
                From = fromEmail,
                Subject = request.Subject,
                PlainTextContent = request.PlainTextContent,
                HtmlContent = request.HtmlContent
            };

            Func<MessageRequest, bool> matchesExpected = r => r.To == expected.To &&
                                                                r.From == expected.From &&
                                                                r.Subject == expected.Subject &&
                                                                r.PlainTextContent == expected.PlainTextContent &&
                                                                r.HtmlContent == expected.HtmlContent;

            messageProcessorMock.Setup(processor => processor.Send(It.IsAny<MessageRequest>())).ReturnsAsync(It.IsAny<SendGrid.Response>()).Verifiable();

            var sut = new SendEmailHandler(messageProcessorMock.Object);
            sut.Handle(request, It.IsAny<CancellationToken>());

            messageProcessorMock.Verify(processor => processor.Send(It.Is<MessageRequest>(r => matchesExpected(r))), Times.Once);
        }
    }
}
