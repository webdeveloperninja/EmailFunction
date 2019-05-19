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
                Subject = "Subject",
                PlainTextContent = "Plain Text",
            };

            var expected = new MessageRequest
            {
                To = request.To,
                From = fromEmail,
                Subject = request.Subject,
                Content = request.PlainTextContent,
            };

            Func<MessageRequest, bool> matchesExpected = r => r.To == expected.To &&
                                                                r.From == expected.From &&
                                                                r.Subject == expected.Subject &&
                                                                r.Content == expected.Content;

            messageProcessorMock.Setup(processor => processor.Send(It.IsAny<MessageRequest>())).ReturnsAsync(It.IsAny<SendGrid.Response>()).Verifiable();

            var sut = new SendEmailHandler(messageProcessorMock.Object);
            sut.Handle(request, It.IsAny<CancellationToken>());

            messageProcessorMock.Verify(processor => processor.Send(It.Is<MessageRequest>(r => matchesExpected(r))), Times.Once);
        }
    }
}
