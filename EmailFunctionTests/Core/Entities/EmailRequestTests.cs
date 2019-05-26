namespace EmailFunctionTests.Core.Entities
{
    using EmailFunction.Core.Entities;
    using EmailFunction.Core.ValueObjects;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class EmailRequestTests
    {
        [TestMethod]
        public void ShouldCreateEmailRequest()
        {
            var fromAddress = "robert@test.com";
            var fromName = "Robert";
            var toAddress = "to@test.com";
            var subject = "Subject";
            var plainTextConent = "Content";

            var sut = new EmailRequest(fromAddress, fromName, toAddress);
            sut.Subject = subject;
            sut.PlainTextContent = plainTextConent;

            Assert.AreEqual(fromAddress, sut.FromAddress.Address);
            Assert.AreEqual(toAddress, sut.ToAddress.Address);
            Assert.IsInstanceOfType(sut.FromAddress, typeof(Email));
            Assert.IsInstanceOfType(sut.ToAddress, typeof(Email));
            Assert.AreEqual(subject, sut.Subject);
            Assert.AreEqual(plainTextConent, sut.PlainTextContent);
        }

        [TestMethod]
        public void ShouldThrowArgumentNullException_GivenRequestWithNullFromAddress()
        {
            var fromAddress = null as string;
            var fromName = "Robert";
            var toAddress = "to@test.com";

            Assert.ThrowsException<ArgumentNullException>(() => new EmailRequest(fromAddress, fromName, toAddress));
        }

        [TestMethod]
        public void ShouldThrowArgumentNullException_GivenRequestWithNullFromName()
        {
            var fromAddress = "robert@test.com";
            var fromName = null as string;
            var toAddress = "to@test.com";

            Assert.ThrowsException<ArgumentNullException>(() => new EmailRequest(fromAddress, fromName, toAddress));
        }

        [TestMethod]
        public void ShouldThrowArgumentNullException_GivenRequestWithNullToAddress()
        {
            var fromAddress = "robert@test.com";
            var fromName = "Robert";
            var toAddress = null as string;

            Assert.ThrowsException<ArgumentNullException>(() => new EmailRequest(fromAddress, fromName, toAddress));
        }
    }
}
