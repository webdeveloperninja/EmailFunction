namespace EmailFunction.Models
{
    public class EmailRequest
    {
        public string To { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string PlainTextContent { get; set; }
        public string HtmlContent { get; set; }
    }
}
