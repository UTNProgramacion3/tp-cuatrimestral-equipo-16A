using Business.Interfaces;
using Business.Managers;
using DataAccess;
using Domain.Entities;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Linq;
using Unity;

namespace TPCuatrimestral_equipo_16A
{
    public class Global : System.Web.HttpApplication
    {
        public static IUnityContainer Container { get; private set; }
        protected void Application_Start(object sender, EventArgs e)
        {
            Container = new UnityContainer();

            Container.RegisterType<IEmpleadoManager, EmpleadoManager>();
            Container.RegisterType<IDireccionManager, DireccionManager>();
            Container.RegisterType<IPersonaManager, PersonaManager>();
            Container.RegisterType<IPacienteManager, PacienteManager>();
            Container.RegisterType<IMedicoManager, MedicoManager>();
            Container.RegisterType<IUsuarioManager, UsuarioManager>();
            Container.RegisterType<IJornadaManager, JornadaManager>();
            Container.RegisterType<ISedeManager, SedeManager>();
        }
    }
}