using EmailFunction.Core.Interfaces;
using System;

namespace EmailFunction.Infrastructure
{
    public class Configuration : IConfiguration
    {
        public string FromAddress => Environment.GetEnvironmentVariable("FROM_ADDRESS", EnvironmentVariableTarget.Process);
        public string FromName => Environment.GetEnvironmentVariable("FROM_NAME", EnvironmentVariableTarget.Process);
        public string SendgridKey => Environment.GetEnvironmentVariable("SENDGRID_KEY", EnvironmentVariableTarget.Process);
    }
}
