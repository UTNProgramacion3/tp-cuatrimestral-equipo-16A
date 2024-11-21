using Business.Interfaces;
using Business.Managers;
using DataAccess;
using Domain.Entities;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Linq;
using Unity;
using Business.Services;

namespace TPCuatrimestral_equipo_16A
{
    public class Global : System.Web.HttpApplication
    {
        public static IUnityContainer Container { get; private set; }
        protected void Application_Start(object sender, EventArgs e)
        {
            Environment.SetEnvironmentVariable("SMTP_SERVER", ConfigurationManager.AppSettings["SMTP_SERVER"]);
            Environment.SetEnvironmentVariable("SMTP_PORT", ConfigurationManager.AppSettings["SMTP_PORT"]);
            Environment.SetEnvironmentVariable("SMTP_USER", ConfigurationManager.AppSettings["SMTP_USER"]);
            Environment.SetEnvironmentVariable("SMTP_PASSWORD", ConfigurationManager.AppSettings["SMTP_PASSWORD"]);
            Environment.SetEnvironmentVariable("BASE_URL", ConfigurationManager.AppSettings["BASE_URL"]);
            Environment.SetEnvironmentVariable("APP_NAME", ConfigurationManager.AppSettings["APP_NAME"]);
            if (Environment.GetEnvironmentVariable("SMTP_USER")== null ||
                Environment.GetEnvironmentVariable("SMTP_PASSWORD") == null)
            {
                throw new Exception("No se ha configurado usuario / contraseña para envío de mails");
            }
            Container = new UnityContainer();

            Container.RegisterType<IEmpleadoManager, EmpleadoManager>();
            Container.RegisterType<IDireccionManager, DireccionManager>();
            Container.RegisterType<IPersonaManager, PersonaManager>();
            Container.RegisterType<IPacienteManager, PacienteManager>();
            Container.RegisterType<IMedicoManager, MedicoManager>();
            Container.RegisterType<IUsuarioManager, UsuarioManager>();
            Container.RegisterType<IJornadaManager, JornadaManager>();
            Container.RegisterType<ISedeManager, SedeManager>();
            Container.RegisterType<IEspecialidadManager, EspecialidadManager>();
            Container.RegisterType<IEmailManager, EmailManager>();
            Container.RegisterType<ISeguridadService, SeguridadService>();
        }
    }
}