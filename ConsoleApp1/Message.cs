using MimeKit;
using MimeKit.Text;
using System;

namespace ConsoleApp1
{
    public class Message : MimeMessage
    {
        public Message(string sender, string recipient, string subject, string body, DateTime date)
        {            
            From.Add(new MailboxAddress("", sender));
            To.Add(new MailboxAddress("", recipient));
            Subject = subject;
            Body = new TextPart(TextFormat.Plain) { Text = body };
            Date = date;
        }
    }
}
