namespace EmailFunctionTests.Models
{
    using EmailFunction.Exceptions;
    using EmailFunction.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Newtonsoft.Json;
    using System.IO;
    using System.Threading.Tasks;

    [TestClass]
    public class EmailRequestConverterTests
    {
        [TestMethod]
        public async Task Should_ReturnEmailRequest_GivenHttpRequest()
        {
            var request = new EmailRequest
            {
                To = "robert@test.com",
                Subject = "Subject",
                PlainTextContent = "Content"
            };

            Mock<HttpRequest> requestMock = CreateMockRequest(request);
            var sut = new EmailRequestConverter();
            var convertedRequest = await sut.Convert(requestMock.Object);

            Assert.AreEqual(request.To, convertedRequest.To);
            Assert.AreEqual(request.Subject, convertedRequest.Subject);
            Assert.AreEqual(request.PlainTextContent, convertedRequest.PlainTextContent);
        }

        [TestMethod]
        public async Task Should_ThrowRequestConverterException_GivenRequestWithoutSubject()
        {
            var request = new EmailRequest
            {
                To = "robert@test.com",
                PlainTextContent = "Content"
            };

            Mock<HttpRequest> requestMock = CreateMockRequest(request);

            var sut = new EmailRequestConverter();

            await Assert.ThrowsExceptionAsync<RequestConverterException>(() => sut.Convert(requestMock.Object));
        }

        [TestMethod]
        public async Task Should_ThrowRequestConverterException_GivenRequestWithoutTo()
        {
            var request = new EmailRequest
            {
                PlainTextContent = "Content",
                Subject = "Subject"
            };

            Mock<HttpRequest> requestMock = CreateMockRequest(request);

            var sut = new EmailRequestConverter();

            await Assert.ThrowsExceptionAsync<RequestConverterException>(() => sut.Convert(requestMock.Object));
        }

        [TestMethod]
        public async Task Should_ThrowRequestConverterException_GivenRequestWithoutPlainTextContent()
        {
            var request = new EmailRequest
            {
                To = "robert@test.com",
                Subject = "Subject"
            };

            Mock<HttpRequest> requestMock = CreateMockRequest(request);

            var sut = new EmailRequestConverter();

            await Assert.ThrowsExceptionAsync<RequestConverterException>(() => sut.Convert(requestMock.Object));
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
