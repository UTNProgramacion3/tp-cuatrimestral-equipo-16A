using Business.Interfaces;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;

namespace Business.Managers
{
    public class EmailManager : IEmailManager
    {
        #region Private properties
        private readonly DBManager _DBManager;
        private readonly string _SMTP_SERVER;
        private readonly int _SMTP_PORT;
        private readonly string _SMTP_USER;
        private readonly string _SMTP_PASSWORD;
        private readonly string _APP_NAME;
        private SmtpClient _SmtpClient;
        #endregion

        #region Builder
        public EmailManager(DBManager manager)
        {
            _SMTP_SERVER = Environment.GetEnvironmentVariable("SMTP_SERVER");
            _SMTP_PORT = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT"));
            _SMTP_USER = Environment.GetEnvironmentVariable("SMTP_USER");
            _SMTP_PASSWORD = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
            _APP_NAME = Environment.GetEnvironmentVariable("APP_NAME");
            _SmtpClient = new SmtpClient(_SMTP_SERVER, _SMTP_PORT)
            {
                Credentials = new System.Net.NetworkCredential(_SMTP_USER, _SMTP_PASSWORD),
                EnableSsl = true
            };
            _DBManager = manager;
        }
        #endregion

        #region Public methods
        public async Task EnviarEmail(string destinatario, string subject, string body)
        {
            MailAddress from = new MailAddress(_SMTP_USER,
              _APP_NAME, System.Text.Encoding.UTF8);

            MailAddress to = new MailAddress(destinatario);
            var mail = new MailMessage(from, to);

            mail.Subject = subject;
            mail.Body = body;

            try
            {
                await _SmtpClient.SendMailAsync(mail);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                mail.Dispose();
                _SmtpClient.Dispose();
            }
        }

        #endregion

        #region Private properties
       

        #endregion
    }
}
