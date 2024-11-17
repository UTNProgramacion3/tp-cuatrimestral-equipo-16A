using Business.Interfaces;
using Business.Managers;
using DataAccess;
using Domain.Entities;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace TPCuatrimestral_equipo_16A.Pages
{
	public partial class Home : System.Web.UI.Page
	{

        private IEmailManager _emailManager;
        private IPacienteManager _pacienteManager;

        private void InitDependencies()
        {
            IUnityContainer unityContainer;
            _emailManager = (IEmailManager)Global.Container.Resolve(typeof(IEmailManager));
            _pacienteManager = (IPacienteManager)Global.Container.Resolve(typeof(IPacienteManager));
        }

        protected void Page_Load(object sender, EventArgs e)
		{
            InitDependencies();
		}

        protected void Button1_Click(object sender, EventArgs e)
        {
            //_emailManager.EnviarMailValidacionNuevaCuenta("escuderopablo.m@gmail.com", 1);
            var res = _pacienteManager.ObtenerPorId(1);
        }
    }
}