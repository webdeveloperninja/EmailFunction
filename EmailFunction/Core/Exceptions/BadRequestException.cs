namespace EmailFunction.Core.Exceptions
{
    using System;

    public class BadRequestException : Exception
    {
        private const string _message = "Email request not valid";

        public BadRequestException() : base(_message)
        {
        }
    }
}
