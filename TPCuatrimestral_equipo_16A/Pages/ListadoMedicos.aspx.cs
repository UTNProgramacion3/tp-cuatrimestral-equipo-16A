using Business.Interfaces;
using Business.Services;
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
    public partial class ListadoMedicos : System.Web.UI.Page
    {
        private IMedicoManager _medicoManager;
        private Usuario _usuario;

        private void InitDependencies()
        {
            IUnityContainer unityContainer;
            _medicoManager = (IMedicoManager)Global.Container.Resolve(typeof(IMedicoManager));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            InitDependencies();

            if (Session["UserLogueado"] != null)
            {
                _usuario = (Usuario)Session["UserLogueado"];
                if(!IsPostBack)
                {
                    CargarMedicos();
                    CargarEspecialidades();
                }
            }else
            {
                Response.Redirect("~/Pages/Home.aspx");
            }
        }
        private void CargarEspecialidades()
        {

            List<Especialidad> especialidades = _medicoManager.ObtenerTodasEspecialidades();


            ddlEspecialidad.DataSource = especialidades;
            ddlEspecialidad.DataTextField = "Nombre"; 
            ddlEspecialidad.DataValueField = "Id";   
            ddlEspecialidad.DataBind();

            ddlEspecialidad.Items.Insert(0, new ListItem("Seleccionar", ""));
            ddlEspecialidad.Items.Insert(1, new ListItem("Todas las especialidades", "0"));
        }

        private void CargarMedicos()
        {
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string matricula = txtMatricula.Text.Trim();
            int especialidadId = string.IsNullOrEmpty(ddlEspecialidad.SelectedValue) ? 0 : int.Parse(ddlEspecialidad.SelectedValue);


            var medicos = _medicoManager.ObtenerTodosConFiltro(nombre, apellido, matricula, especialidadId);

            gvMedicos.DataSource = medicos;
            gvMedicos.DataBind();
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarMedicos();
        }

        protected void LimpiarFiltros()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtMatricula.Text = string.Empty;

            ddlEspecialidad.SelectedIndex = 0; 

            CargarMedicos(); 
        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
        }

        private void CargarEspecialidades(string especialidadSeleccionada)
        {
            List<Especialidad> especialidades = _medicoManager.ObtenerTodasEspecialidades();

            ddlEspecialidad.DataSource = especialidades;
            ddlEspecialidad.DataTextField = "Nombre";
            ddlEspecialidad.DataValueField = "Id";
            ddlEspecialidad.DataBind();

            ddlEspecialidad.SelectedValue = especialidadSeleccionada;
        }

        protected void gvMedicos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvMedicos.EditIndex = e.NewEditIndex;
            CargarMedicos();
        }

        protected void gvMedicos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvMedicos.Rows[e.RowIndex];

            int nuevaEspecialidadId = int.Parse(((DropDownList)row.FindControl("ddlEspecialidad")).SelectedValue);

            string matricula = gvMedicos.DataKeys[e.RowIndex].Value.ToString();

            _medicoManager.ActualizarEspecialidad(Convert.ToInt32(matricula), nuevaEspecialidadId);

            gvMedicos.EditIndex = -1;
            CargarMedicos();
        }

        protected void gvMedicos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMedicos.EditIndex = -1;
            CargarMedicos();
        }

        private RolesEnum GetUserRole()
        {
            return (RolesEnum)_usuario.Rol.Id;
        }

        protected void gvMedicos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton editButton = (LinkButton)e.Row.FindControl("EditButton");
                Button saveButton = (Button)e.Row.FindControl("SaveButton");
                Button cancelButton = (Button)e.Row.FindControl("CancelButton");

                RolesEnum userRole = GetUserRole(); 

                bool canEdit = userRole == RolesEnum.Recepcionista || userRole == RolesEnum.Administrador;

                if (gvMedicos.EditIndex == e.Row.RowIndex)
                {
                    if (editButton != null) editButton.Visible = false; 
                    if (saveButton != null) saveButton.Visible = true; 
                    if (cancelButton != null) cancelButton.Visible = true;
                }
                else
                {
                    if (editButton != null) editButton.Visible = canEdit;
                    if (saveButton != null) saveButton.Visible = false;
                    if (cancelButton != null) cancelButton.Visible = false;
                }

                if (gvMedicos.EditIndex == e.Row.RowIndex)
                {
                    DropDownList ddlEspecialidad = (DropDownList)e.Row.FindControl("ddlEspecialidad");
                    if (ddlEspecialidad != null)
                    {
                        List<Especialidad> especialidades = _medicoManager.ObtenerTodasEspecialidades();
                        ddlEspecialidad.DataSource = especialidades;
                        ddlEspecialidad.DataTextField = "Nombre";
                        ddlEspecialidad.DataValueField = "Id";
                        ddlEspecialidad.DataBind();

                        string especialidadId = DataBinder.Eval(e.Row.DataItem, "EspecialidadId").ToString();
                        ddlEspecialidad.SelectedValue = especialidadId;
                    }
                }
            }
        }


        
    }
}