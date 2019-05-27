namespace EmailFunction.Core.DTO
{
    public class EmailRequestDTO
    {
        public string To { get; set; }

        public string Subject { get; set; }

        public string PlainTextContent { get; set; }

        public string HtmlContent { get; set; }
    }
}
