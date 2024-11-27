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

            if (!IsPostBack)
            {
                var tarjetas = new List<dynamic>
        {
            new { Titulo = "Agendar Turno", Descripcion = "Agenda un nuevo turno", Url = "/Pages/SeleccionarPaciente.aspx" },
            new { Titulo = "Modificar Turno", Descripcion = "Modifica un turno existente.", Url = "/Pages/ListadoTurnos.aspx" },
            new { Titulo = "Turnos", Descripcion = "Consulta los Turnos.", Url = "/Pages/ListadoTurnos.aspx" },
            new { Titulo = "Perfil", Descripcion = "Gestiona tu perfil.", Url = "/Pages/Perfil.aspx" },
            new { Titulo = "Sedes", Descripcion = "Administrar las Sedes.", Url = "/Pages/ListarSedes.aspx" },
            new { Titulo = "Especialidades", Descripcion = "Administrar las Especialidades.", Url = "/Pages/Especialidades.aspx" },
            new { Titulo = "Pacientes", Descripcion = "Administrar los datos de pacientes.", Url = "#" },
            new { Titulo = "Medicos", Descripcion = "Administrar los datos de medicos.", Url = "#" },
            new { Titulo = "Usuarios", Descripcion = "Administrar los datos de usuarios.", Url = "#" }
        };

                rptTarjetas.DataSource = tarjetas;
                rptTarjetas.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //_emailManager.EnviarMailValidacionNuevaCuenta("escuderopablo.m@gmail.com", 1);
            var res = _pacienteManager.ObtenerPorId(1);
        }


    }
}