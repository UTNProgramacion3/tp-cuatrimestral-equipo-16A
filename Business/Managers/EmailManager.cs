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
using System.Data.SqlClient;
using Business.Dtos;
using DataAccess.Extensions;

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
        private readonly string _BASE_URL;
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
            _BASE_URL = Environment.GetEnvironmentVariable("BASE_URL");
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
           MailMessage nuevoMail = GenerarNuevoEmail(destinatario, subject, body);

            try
            {
                await _SmtpClient.SendMailAsync(nuevoMail);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                nuevoMail.Dispose();
                _SmtpClient.Dispose();
            }
        }

        /// <summary>
        /// Creación de nuevo token y envío de mail de validación para nueva cuenta
        /// </summary>
        /// <param name="destinatario"></param>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public void EnviarMailValidacionNuevaCuenta(string destinatario, int usuarioId)
        {
            var now = DateTime.Now;
            string tokenExistenteQuery = "SELECT * FROM EmailValidaciones WHERE UsuarioId = @id AND TiempoExpiracion <= @ahora ";

            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@id", usuarioId),
                new SqlParameter("@ahora", now)
            };

            var tokenExistente = _DBManager.ExecuteQuery(tokenExistenteQuery, param);

            if (tokenExistente.GetEntity<EmailValidationDto>() != null)
            {
                throw new Exception("Ya se ha enviado un mail de validacion para este usuario");
            }

            var token = new Guid().ToString();
            var expirationDate = DateTime.Now.AddMinutes(30);

            string query = "INSERT INTO EmailValidaciones (Token, TiempoExpiracion, UsuarioId) VALUES (@Token, @TiempoExpiracion, @UsuarioId)";
            string retrieveData = @"SELECT * FROM EmailValidaciones where UsuarioId = @UsuarioId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Token", token),
                new SqlParameter("@TiempoExpiracion", expirationDate),
                new SqlParameter("@UsuarioId", usuarioId)
            };

            var res = _DBManager.ExecuteNonQueryAndGetData(query, parameters, retrieveData);

            if (res == null)
            {
                throw new Exception("Hubo un error al crear el token de validacion");
            }

            var validacion = res.GetEntity<EmailValidationDto>();

            var url = $"${_BASE_URL}/validar-cuenta/{validacion.Token}";

            GenerarNuevoEmail(destinatario, $"Validación de cuenta - ${_APP_NAME}", $"Haga click en el siguiente link para validar su cuenta: {url}");
        }

        private MailMessage GenerarNuevoEmail(string destinatario, string subject, string body)
        {
            MailAddress from = new MailAddress(_SMTP_USER,
              _APP_NAME, System.Text.Encoding.UTF8);

            MailAddress to = new MailAddress(destinatario);
            var mail = new MailMessage(from, to);

            mail.Subject = subject;
            mail.Body = body;

            return mail;
        }

        #endregion

        #region Private properties


        #endregion
    }
}
