using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace NNI.PayerPortal.Domain.Concrete
{
    public class EmailMessage
    {
        public string MailToAddress { get; set; }
        public string MailFromAddress { get; set; }
        public string MessageSubject { get; set; }
        public string MessageBody { get; set; }                 
    }

    public class SendMail
    {
        private EmailMessage  emailMessage;

        public SendMail(EmailMessage message)
        {
            emailMessage = message;
        }

        public void Send()
        {
            // Write File To Disk
            if (MailSettings.WriteAsFile)
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.EnableSsl = MailSettings.UseSsl;
                    smtpClient.Host = MailSettings.ServerName;
                    smtpClient.Port = MailSettings.ServerPort;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(MailSettings.Username, MailSettings.Password);
                    
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = MailSettings.FileLocation;
                    smtpClient.EnableSsl = MailSettings.UseSsl;


                    MailMessage mailMessage = new MailMessage(
                        emailMessage.MailFromAddress,   // From
                        emailMessage.MailToAddress,     // To
                        emailMessage.MessageSubject,    // Subject
                        emailMessage.MessageBody        // Body
                    );

                    mailMessage.BodyEncoding = Encoding.ASCII;
                    smtpClient.Send(mailMessage);
                }
            }
            // Send Via SMTP
            if (MailSettings.SendAsSmtp)
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.EnableSsl = MailSettings.UseSsl;
                    smtpClient.Host = MailSettings.ServerName;
                    smtpClient.Port = MailSettings.ServerPort;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(MailSettings.Username, MailSettings.Password);

                    MailMessage mailMessage = new MailMessage(
                        emailMessage.MailFromAddress,   // From
                        emailMessage.MailToAddress,     // To
                        emailMessage.MessageSubject,    // Subject
                        emailMessage.MessageBody        // Body
                    );
                    // SEND
                    smtpClient.Send(mailMessage);
                }
            }
        }        
    }
}
