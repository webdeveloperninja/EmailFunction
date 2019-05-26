namespace EmailFunctionTests.Core.Commands
{
    using EmailFunction.Core.Commands;
    using EmailFunction.Core.Entities;
    using EmailFunction.Core.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    [TestClass]
    public class SendEmailTests
    {
        [TestMethod]
        public async Task Handle_CallsMessageProcessor_WhenGivenValidRequest()
        {
            var messageProcessorMock = new Mock<IMessageProcessor>();
            messageProcessorMock.Setup(m => m.Send(It.IsAny<EmailRequest>()))
                .ReturnsAsync(It.IsAny<SendGrid.Response>()).Verifiable();

            var sut = new SendEmailHandler(messageProcessorMock.Object);

            var request = new EmailRequest(fromAddress: "from@test.com", fromName: "Robert", toAddress: "to@test.com");

            var response = await sut.Handle(request, It.IsAny<CancellationToken>());

            Func<EmailRequest, bool> matches = r => r.FromAddress == request.FromAddress &&
                                                    r.FromName == request.FromName &&
                                                    r.ToAddress == request.ToAddress;

            messageProcessorMock.Verify(m => m.Send(It.Is<EmailRequest>(r => matches(r))));
            messageProcessorMock.VerifyAll();
        }
    }
}
