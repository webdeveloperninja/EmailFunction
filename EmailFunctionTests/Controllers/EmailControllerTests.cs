namespace EmailFunctionTests.Controllers
{
    using EmailFunction.Controllers;
    using EmailFunction.Core.DTO;
    using EmailFunction.Core.Entities;
    using EmailFunction.Core.Exceptions;
    using EmailFunction.Core.Interfaces;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    [TestClass]
    public class EmailControllerTests
    {
        [TestMethod]
        public async Task Execute_CreatesEmailRequest_FromHttpRequest()
        {
            var deserializerMock = new Mock<IRequestDeserializer>();
            var mediatorMock = new Mock<IMediator>();
            var configurationMock = new Mock<IConfiguration>();

            var emailRequestResponse = "Successfully Send Email";
            var fromAddress = "from@from.com";
            var fromName = "from name";

            var requestDTO = new EmailRequestDTO
            {
                To = "robert@test.com",
                Subject = "Subject",
                PlainTextContent = "Email Content"
            };

            deserializerMock.Setup(d => d.Deserialize<EmailRequestDTO>(It.IsAny<HttpRequest>())).ReturnsAsync(requestDTO);
            mediatorMock.Setup(m => m.Send(It.IsAny<EmailRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(emailRequestResponse).Verifiable();
            configurationMock.Setup(c => c.FromAddress).Returns(fromAddress);
            configurationMock.Setup(c => c.FromName).Returns(fromName);

            var sut = new EmailController(deserializerMock.Object, mediatorMock.Object, configurationMock.Object);

            Func<EmailRequest, bool> matchesRequest = request => request.ToAddress.Address == requestDTO.To &&
                                                          request.PlainTextContent == requestDTO.PlainTextContent &&
                                                          request.Subject == requestDTO.Subject;

            var result = await sut.Execute(requestDTO);

            mediatorMock.Verify(m => m.Send(It.Is<EmailRequest>(r => matchesRequest(r)), It.IsAny<CancellationToken>()), Times.Once());

            Assert.AreEqual(emailRequestResponse, result.Response);
        }

        [TestMethod]
        public async Task Execute_ReturnsBadRequest_WhenEmailRequestHasNoTo()
        {
            var deserializerMock = new Mock<IRequestDeserializer>();
            var mediatorMock = new Mock<IMediator>();
            var configurationMock = new Mock<IConfiguration>();

            var emailRequestResponse = "Successfully Send Email";
            var fromAddress = "from@from.com";
            var fromName = "from name";

            var requestDTO = new EmailRequestDTO
            {
                Subject = "Subject",
                PlainTextContent = "Email Content"
            };

            deserializerMock.Setup(d => d.Deserialize<EmailRequestDTO>(It.IsAny<HttpRequest>())).ReturnsAsync(requestDTO);
            mediatorMock.Setup(m => m.Send(It.IsAny<EmailRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(emailRequestResponse).Verifiable();
            configurationMock.Setup(c => c.FromAddress).Returns(fromAddress);
            configurationMock.Setup(c => c.FromName).Returns(fromName);

            var sut = new EmailController(deserializerMock.Object, mediatorMock.Object, configurationMock.Object);

            Func<EmailRequest, bool> matchesRequest = request => request.ToAddress.Address == requestDTO.To &&
                                                          request.PlainTextContent == requestDTO.PlainTextContent &&
                                                          request.Subject == requestDTO.Subject;

            

            mediatorMock.Verify(m => m.Send(It.Is<EmailRequest>(r => matchesRequest(r)), It.IsAny<CancellationToken>()), Times.Never());
            await Assert.ThrowsExceptionAsync<BadRequestException>(() => sut.Execute(requestDTO));
        }

        [TestMethod]
        public async Task Execute_ReturnsArgumentNullException_WhenFromAddressIsNull()
        {
            var deserializerMock = new Mock<IRequestDeserializer>();
            var mediatorMock = new Mock<IMediator>();
            var configurationMock = new Mock<IConfiguration>();

            var emailRequestResponse = "Successfully Send Email";

            var requestDTO = new EmailRequestDTO
            {
                Subject = "Subject",
                To = "to@test.com",
                PlainTextContent = "Email Content"
            };

            deserializerMock.Setup(d => d.Deserialize<EmailRequestDTO>(It.IsAny<HttpRequest>())).ReturnsAsync(requestDTO);
            mediatorMock.Setup(m => m.Send(It.IsAny<EmailRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(emailRequestResponse).Verifiable();
            configurationMock.Setup(c => c.FromAddress).Returns(null as string);
            configurationMock.Setup(c => c.FromName).Returns("From Name");

            var sut = new EmailController(deserializerMock.Object, mediatorMock.Object, configurationMock.Object);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.Execute(requestDTO));
        }

        [TestMethod]
        public async Task Execute_ReturnsArgumentNullException_WhenFromNameIsNull()
        {
            var deserializerMock = new Mock<IRequestDeserializer>();
            var mediatorMock = new Mock<IMediator>();
            var configurationMock = new Mock<IConfiguration>();

            var emailRequestResponse = "Successfully Send Email";

            var requestDTO = new EmailRequestDTO
            {
                Subject = "Subject",
                To = "to@test.com",
                PlainTextContent = "Email Content"
            };

            deserializerMock.Setup(d => d.Deserialize<EmailRequestDTO>(It.IsAny<HttpRequest>())).ReturnsAsync(requestDTO);
            mediatorMock.Setup(m => m.Send(It.IsAny<EmailRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(emailRequestResponse).Verifiable();
            configurationMock.Setup(c => c.FromAddress).Returns("test@test.com");
            configurationMock.Setup(c => c.FromName).Returns(null as string);

            var sut = new EmailController(deserializerMock.Object, mediatorMock.Object, configurationMock.Object);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => sut.Execute(requestDTO));
        }

        private static Mock<HttpRequest> CreateMockRequest(object body)
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);

            var json = JsonConvert.SerializeObject(body);

            sw.Write(json);
            sw.Flush();

            ms.Position = 0;

            var mockRequest = new Mock<HttpRequest>();
            mockRequest.Setup(x => x.Body).Returns(ms);

            return mockRequest;
        }
    }
}
