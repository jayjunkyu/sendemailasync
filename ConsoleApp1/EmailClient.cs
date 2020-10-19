using MimeKit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class EmailClient
    {
        // password should be encrypted this is just for demontration purposes

        private static readonly string sender = "";
        private static readonly string encryptedPassword = "";
        private static readonly string clientName = "smtp.gmail.com";
        private static readonly int port = 587;


        // for flexibility uer can send a list of messages
        // or a single message
        public static async Task SendMessage(List<Message> messages)
        {
            foreach (var message in messages)
            {
                var successFlag = await EmailClient.SendMessageAsync(message);
                DBHelper.SaveMailToDB(message, successFlag);
            }
        }

        public static async Task SendMessage(Message message)
        {
            var successFlag = await EmailClient.SendMessageAsync(message);
            DBHelper.SaveMailToDB(message, successFlag);            
        }

        public static async Task ResendFailedMails()
        {
            var failedMails = DBHelper.ExecuteStoredProcedure("spMail_GetAllFail", true);

            while (failedMails.Count > 0)
            {
                Message currMessage;
                string mailID;

                foreach (var mail in failedMails)
                {
                    currMessage = new Message(mail["Sender"].ToString(), mail["Recipient"].ToString(), mail["Subject"].ToString(), mail["Body"].ToString(), DateTime.Parse(mail["Date"].ToString()));
                    mailID = mail["MailID"].ToString();
                    await EmailClient.SendFailedMail(mailID, currMessage);
                }

                failedMails = DBHelper.ExecuteStoredProcedure("spMail_GetAllFail", true);
            }
        }

        // below private async methods are used in public methods
        // these are private so users do not need to worry about how client is connected
        private static async Task<bool> SendMessageAsync(Message message)
        {
            try
            {
                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    await smtp.ConnectAsync(clientName, port);
                    await smtp.AuthenticateAsync(sender, encryptedPassword);
                    await smtp.SendAsync(message);
                    await smtp.DisconnectAsync(true);
                    Console.WriteLine("sent");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        private static async Task SendFailedMail(string mailID, Message message)
        {
            var reattempt = await EmailClient.SendMessageAsync(message);
            DBHelper.UpdateMailToDB(mailID, reattempt);
        }
    }
}
