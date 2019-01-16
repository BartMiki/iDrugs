using Common.Utils;
using System.Net.Mail;
using WebApp.Interfaces;
using static Common.Handlers.StaticDatabaseExceptionHandler;

namespace WebApp.Service
{
    public class PrescriptionEmailSenderService : IEmailSenderService
    {
        private static readonly string _sender = "noreply@idrugs.com";

        public Result SendEmail(string sendTo, string subjet, string body)
        {
            var result = Try(() =>
            {
                if (string.IsNullOrWhiteSpace(sendTo)) return;

                var mail = new MailMessage(_sender, sendTo)
                {
                    Subject = subjet,
                    Body = body
                };

                using (var client = new SmtpClient
                {
                    Port = 25,
                    DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                    PickupDirectoryLocation = @"C:\Emails"
                })
                {
                    client.Send(mail);
                }
            }, GetType());

            return result;
        }
    }
}
