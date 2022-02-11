using System;
using System.Configuration;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using CarLeasePPT.Utility;

//using MailKit.Security;

namespace CarLeasePPT.EmailClient
{
    public static class Sender
    {
        #region Properties

        private static string MailSenderAddress =>
            ConfigurationManager.AppSettings["MailSenderAddress"] ?? string.Empty;

        private static string MailSenderName => ConfigurationManager.AppSettings["MailSenderName"] ?? string.Empty;
        private static string MailServer => ConfigurationManager.AppSettings["MailServer"] ?? "mail.us.hex.com";

        private static int MailServerPort =>
            int.TryParse(ConfigurationManager.AppSettings["MailServerPort"], out var port) ? port : 25;

        #endregion

        #region Public Methods and Operators

        public static void Send(string toName, string toEmailAddress, string subject, string body)
        {
            try
            {
                var m = new MimeMessage();
                m.From.Add(new MailboxAddress(MailSenderName, MailSenderAddress));
                m.To.Add(new MailboxAddress(toName, toEmailAddress));
                m.Subject = subject;
                m.Body = new TextPart(TextFormat.Text)
                {
                    Text = body
                };

                using (var c = new SmtpClient())
                {
                    c.Connect(MailServer, MailServerPort, false);
                    c.Send(m);
                    c.Disconnect(true);
                }
            }
            catch (Exception exception)
            {
                Log.Instance.Error(exception);
            }
        }

        #endregion
    }
}