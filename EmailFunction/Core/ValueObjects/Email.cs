namespace EmailFunction.Core.ValueObjects
{
    using System;

    public class Email
    {
        public string Address { get; private set; }

        public Email(string emailAddress)
        {
            Address = emailAddress ?? throw new ArgumentNullException(nameof(emailAddress));
        }
    }
}
