using System.Net.Mail;
using System.Net;
using System.Text;

namespace SNJGlobalAPI.GeneralServices
{
    public class EmailService
    {
        public static async Task<string> SendAsync(EmailOptions options)
        {
            try
            {
                //creating mail message
                using (MailMessage message = new MailMessage())
                {
                    //initial mail setting
                    message.From = new MailAddress(SmtpConfig.senderAddress, SmtpConfig.senderDisplayName);
                    message.Subject = options.subject;
                    message.Body = options.body;
                    message.IsBodyHtml = SmtpConfig.isBodyHtml;
                    message.BodyEncoding = Encoding.Default;

                    //attaching file if any
                    if (options.atchByte is not null)
                    {
                        //creating attachment object
                        Attachment attachment = new Attachment(new MemoryStream(options.atchByte), $"{options.attchedWhat}.pdf");
                        message.Attachments.Add(attachment);
                    }

                    //adding Recipient(s)
                    foreach (var recipient in options.recipients)
                    {
                        if (!string.IsNullOrEmpty(recipient))
                            message.To.Add(recipient);
                    }

                    //creating object of network credentials
                    var credentials = new NetworkCredential(SmtpConfig.userName, SmtpConfig.password);

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = SmtpConfig.host;
                        smtp.Port = SmtpConfig.port;
                        smtp.EnableSsl = SmtpConfig.enableSsl;
                        smtp.Credentials = credentials;

                        //now executing mail send method
                        await smtp.SendMailAsync(message);
                    }//smtp client using 
                    return "Success";
                }//mail message using
            }//try
            catch (Exception ex)
            {
                return ex.Message;
            }
        }//send email async method 
    }//class email service

    public class SmtpConfig
    {
        public static string senderAddress { get; set; }
        public static string senderDisplayName { get; set; }
        public static string userName { get; set; }
        public static string password { get; set; }
        public static string host { get; set; }
        public static int port { get; set; }
        public static bool enableSsl { get; set; }
        public static bool useDefaultCredentials { get; set; }
        public static bool isBodyHtml { get; set; }
    }

    public class EmailOptions
    {
        public List<string> recipients { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public byte[] atchByte { get; set; }
        public string attchedWhat { get; set; }
    }
}
