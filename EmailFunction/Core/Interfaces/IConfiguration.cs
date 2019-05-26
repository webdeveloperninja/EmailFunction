namespace EmailFunction.Core.Interfaces
{
    public interface IConfiguration
    {
        string FromAddress { get; }

        string FromName { get; }

        string SendgridKey { get; }
    }
}
