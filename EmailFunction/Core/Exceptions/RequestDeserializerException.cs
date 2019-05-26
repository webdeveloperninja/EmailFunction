namespace EmailFunction.Core.Exceptions
{
    using Microsoft.AspNetCore.Http;
    using System;

    public class RequestDeserializerException : Exception
    {
        private const string _message = "Could not deserialize email request";
        private const string _dataKey = "email_request";

        public RequestDeserializerException(HttpRequest request) : base(_message)
        {
            this.Data[_dataKey] = request;
        }
    }
}
