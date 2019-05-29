# EmailFunction

Add the following variables to your local.settings.json or enviroment variables 
```
    "SENDGRID_KEY": {{ Your Sendgrid Key Here }},
    "FROM_ADDRESS": "{{ Your From Address }}",
    "FROM_NAME": "{{ From Name }}"
```
Post to endpoint `/api/Email` with following body content.
```
{
	"To": "{{ Your To Address Here }}",
	"PlainTextContent": "{{ Your Plain Text Content Here }}",
	"Subject": "{{ Your Subject Here }}",
	"HtmlContent": "{{ Your Html Content Here }}"
}
```

Example Request
```
{
	"To": "test@gmail.com",
	"PlainTextContent": "Content test asdf asfd sdfasdfa sdfa",
	"Subject": "tes asdf sadf sadf sdafsdfa t",
	"HtmlContent": "<a href='www.google.com'>Google</a>"
}
```
