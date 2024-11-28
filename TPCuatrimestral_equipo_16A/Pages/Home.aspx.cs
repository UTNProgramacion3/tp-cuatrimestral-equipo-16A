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


        //private void InitDependencies()
        //{
        //}

        protected void Page_Load(object sender, EventArgs e)
		{
            //InitDependencies();

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
            new { Titulo = "Pacientes", Descripcion = "Administrar los datos de pacientes.", Url = "/Pages/ListadoPacientes.aspx" },
            new { Titulo = "Medicos", Descripcion = "Administrar los datos de medicos.", Url = "/Pages/ListadoMedicos.aspx" },
            new { Titulo = "Usuarios", Descripcion = "Administrar los datos de usuarios.", Url = "/Pages/MenuUsuario.aspx" },
            new { Titulo = "Permisos y Modulos", Descripcion = "Administrar Permisos de modulos.", Url = "/Pages/AdmPermisos.aspx"},
            new { Titulo = "Permisos y Roles", Descripcion = "Administrar Permisos de roles.", Url = "/Pages/RolesPermisos.aspx"},
            new { Titulo = "Crear Usuario", Descripcion = "Creacion de cuentas", Url= "/Pages/CrearNuevoUsuario.aspx"}
        };

                rptTarjetas.DataSource = tarjetas;
                rptTarjetas.DataBind();
            }
        }
    }
}