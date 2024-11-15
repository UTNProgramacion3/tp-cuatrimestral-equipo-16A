using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace TPCuatrimestral_equipo_16A
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Environment.SetEnvironmentVariable("SMTP_SERVER", ConfigurationManager.AppSettings["SMTP_SERVER"]);
            Environment.SetEnvironmentVariable("SMTP_PORT", ConfigurationManager.AppSettings["SMTP_PORT"]);
            Environment.SetEnvironmentVariable("SMTP_USER", ConfigurationManager.AppSettings["SMTP_USER"]);
            Environment.SetEnvironmentVariable("SMTP_PASSWORD", ConfigurationManager.AppSettings["SMTP_PASSWORD"]);
            if (Environment.GetEnvironmentVariable("SMTP_USER")== null ||
                Environment.GetEnvironmentVariable("SMTP_PASSWORD") == null)
            {
                throw new Exception("No se ha configurado usuario / contraseña para envío de mails");
            }
        }
    }
}