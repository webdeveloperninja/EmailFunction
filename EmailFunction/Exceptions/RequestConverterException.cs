namespace EmailFunction.Exceptions
{
    using Microsoft.AspNetCore.Http;
    using System;

    public class RequestConverterException : Exception
    {
        private const string _message = "Could not deserialize email request";
        private const string _dataKey = "email_request";

        public RequestConverterException(HttpRequest request) : base(_message)
        {
            this.Data[_dataKey] = request;
        }
    }
}
