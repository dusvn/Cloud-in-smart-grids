using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HealthMonitoringService
{
    public class EmailErrorSender
    {

        public void SendEmail(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                TargetName = "STARTTLS/smtp.gmail.com",
                Credentials = new NetworkCredential("drsprodavnica@gmail.com", "quhm uutx gnwn mxlg")
            };

            try
            {
                client.Send(
                new MailMessage(from: "drsprodavnica@gmail.com",
                                to: email,
                                subject,
                                message
                                ));

            }
            catch (Exception)
            {
                Trace.WriteLine("Error occured while sending mail...", "ERROR");

            }


        }

    }
}
