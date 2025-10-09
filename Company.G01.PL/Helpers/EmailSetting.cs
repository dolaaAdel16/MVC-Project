using System.Net;
using System.Net.Mail;

namespace Company.G01.PL.Helpers
{
    public class EmailSetting
    {
        public static bool SendEmail( Email email)
        {
            // Mail Server : Gmail
            // SMTP : Simple Mail Transfer Protocol

            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("steve.addams.ta@gmail.com", "zeyujzauovdbvjhi");
                client.Send("steve.addams.ta@gmail.com", email.To, email.Subject, email.Body);

                return true;
            }
            catch ( Exception e)
            {
                return false; 
            }
        }
    }
}
