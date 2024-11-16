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

        private readonly PacienteManager _pacienteManager;
        private readonly MedicoManager _medicoManager;

        public Home()
        {
            _pacienteManager = (PacienteManager)Global.Container.Resolve(typeof(PacienteManager));
            _medicoManager = (MedicoManager)Global.Container.Resolve(typeof(MedicoManager));
        }

        protected void Page_Load(object sender, EventArgs e)
		{

		}

        protected void Button1_Click(object sender, EventArgs e)
        {

            var res = _pacienteManager.ObtenerTodos();
        }
    }
}