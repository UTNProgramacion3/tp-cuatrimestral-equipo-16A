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
using static TPCuatrimestral_equipo_16A.Global;

namespace TPCuatrimestral_equipo_16A.Pages
{
	public partial class Home : System.Web.UI.Page
	{

        private IEmailManager _emailManager;
        private IPacienteManager _pacienteManager;
        private Usuario _usuario;

        private void InitDependencies()
        {
            IUnityContainer unityContainer;
            _emailManager = (IEmailManager)Global.Container.Resolve(typeof(IEmailManager));
            _pacienteManager = (IPacienteManager)Global.Container.Resolve(typeof(IPacienteManager));
            _usuario = GlobalData.UsuarioLogueado;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitDependencies();

            if (!IsPostBack)
            {
                var tarjetas = new List<dynamic>
        {
            new { Titulo = "Agendar Turno", Descripcion = "Agenda un nuevo turno", Url = "/Pages/SeleccionarPaciente.aspx", RolesPermitidos = new List<int> { 1, 2 } },
            new { Titulo = "Reprogramar Turno", Descripcion = "Reprogramar un turno existente.", Url = "/Pages/ListadoTurnos.aspx", RolesPermitidos = new List<int> { 1, 2, 3 } },
            new { Titulo = "Cancelar Turno", Descripcion = "Cancelar un turno.", Url = "/Pages/CancelarTurno.aspx", RolesPermitidos = new List<int> { 1, 2, 3 } },
            new { Titulo = "Turnos", Descripcion = "Consulta los Turnos.", Url = "/Pages/ListadoTurnos.aspx", RolesPermitidos = new List<int> { 1, 2, 3, 4 } },
            new { Titulo = "Perfil", Descripcion = "Gestiona tu perfil.", Url = "/Pages/Perfil.aspx", RolesPermitidos = new List<int> { 1, 2, 3, 4 } },
            new { Titulo = "Sedes", Descripcion = "Administrar las Sedes.", Url = "/Pages/ListarSedes.aspx", RolesPermitidos = new List<int> { 1 } },
            new { Titulo = "Especialidades", Descripcion = "Administrar las Especialidades.", Url = "/Pages/Especialidades.aspx", RolesPermitidos = new List<int> { 1 } },
            new { Titulo = "Pacientes", Descripcion = "Administrar los datos de pacientes.", Url = "/Pages/ListadoPacientes.aspx", RolesPermitidos = new List<int> { 1, 2 } },
            new { Titulo = "Medicos", Descripcion = "Administrar los datos de medicos.", Url = "/Pages/ListadoMedicos.aspx", RolesPermitidos = new List<int> { 1 } },
            new { Titulo = "Usuarios", Descripcion = "Administrar los datos de usuarios.", Url = "/Pages/MenuUsuario.aspx", RolesPermitidos = new List<int> { 1 } },
            new { Titulo = "Permisos y Modulos", Descripcion = "Administrar Permisos de modulos.", Url = "/Pages/AdmPermisos.aspx", RolesPermitidos = new List<int> { 1 } },
            new { Titulo = "Permisos y Roles", Descripcion = "Administrar Permisos de roles.", Url = "/Pages/RolesPermisos.aspx", RolesPermitidos = new List<int> { 1 } },
            new { Titulo = "Crear Usuario", Descripcion = "Creacion de cuentas", Url= "/Pages/CrearNuevoUsuario.aspx", RolesPermitidos = new List<int> { 1 } }
        };

                rptTarjetas.DataSource = tarjetas.Where(t => t.RolesPermitidos.Contains(_usuario.Rol.Id)).ToList();
                rptTarjetas.DataBind();
            }
        }



    }
}