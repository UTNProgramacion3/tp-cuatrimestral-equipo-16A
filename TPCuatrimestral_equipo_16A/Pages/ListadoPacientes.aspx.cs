using Business.Dtos;
using Business.Interfaces;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class ListadoPacientes1 : System.Web.UI.Page
    {
        private IPacienteManager _pacienteManager;
        private Usuario _usuario;

        private void InitDependencies()
        {
            IUnityContainer unityContainer;
            _pacienteManager = (IPacienteManager)Global.Container.Resolve(typeof(IPacienteManager));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            InitDependencies();

            if (Session["UserLogueado"] != null)
            {
                _usuario = (Usuario)Session["UserLogueado"];

                if (_usuario.Rol.Id == (int)RolesEnum.Administrador || _usuario.Rol.Id == (int)RolesEnum.Recepcionista)
                {
                    if(!IsPostBack)
                    { CargarPacientes(); } 
                }else
                {
                    Response.Redirect("~/Pages/Home.aspx");
                }
            }else
            {
                Response.Redirect("~/Pages/Home.aspx");
            }
        }

        private void CargarPacientes()
        {
            var res = _pacienteManager.ObtenerPacientesFiltrados("", "", "", "", "");

            if (res.Success)
            {
                gvPacientes.DataSource = res.Data;
                gvPacientes.DataBind();
            }

        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string documento = txtDocumento.Text.Trim();
            string nroAfiliado = txtNroAfiliado.Text.Trim();
            string obraSocial = txtObraSocial.Text.Trim();

            var response = _pacienteManager.ObtenerPacientesFiltrados(nombre, apellido, documento, obraSocial, nroAfiliado);

            if(response.Success)
            {
                gvPacientes.DataSource = response.Data;
                gvPacientes.DataBind();

            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            LinkButton btnEditar = (LinkButton)sender;
            int personaId = int.Parse(btnEditar.CommandArgument);

            var response = _pacienteManager.ObtenerPacientesFiltrados("", "", "", "", "");
            if(response.Success)
            {
                List<PacienteSimpleDto> pacientes = response.Data;
                PacienteSimpleDto pacienteSeleccionado = pacientes.Find(p => p.PersonaId == personaId);

                Session["EdicionDePaciente"] = pacienteSeleccionado;

                Response.Redirect("EdicionPaciente.aspx");
            }

        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtObraSocial.Text = string.Empty;
            txtNroAfiliado.Text = string.Empty;

            CargarPacientes();
        }
    }
}