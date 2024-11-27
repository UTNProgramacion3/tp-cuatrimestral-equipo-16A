using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Business.Dtos;
using Business.Interfaces;
using Business.Managers;
using DataAccess;
using Domain.Entities;
using Domain.Response;
using Unity;
using Utils;


namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class Especialidades : System.Web.UI.Page
    {


        private DBManager dbManager;
        private IEspecialidadManager _especialidadManager;

        private void InitDependencies()
        {
            IUnityContainer unityContainer;
            _especialidadManager = (IEspecialidadManager)Global.Container.Resolve(typeof(IEspecialidadManager));

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitDependencies();

            if(!IsPostBack)
            {
                CargarEspecialidades();
            }
        }


        private void CargarEspecialidades()
        {
            Response<List<Especialidad>> listaEspecialidades = _especialidadManager.ObtenerTodos();

            try
            {
                dgvEspecialidades.DataSource = listaEspecialidades.Data;
                dgvEspecialidades.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            Session["IdEspecialidad"] = null;
            Session["NombreEspecialidad"] = null;

            btnModificar.Enabled = false;

            if(!string.IsNullOrEmpty(txtBuscarEspecialidad.Text))
            {
                txtBuscarEspecialidad.Text = "";
                CargarEspecialidades();
            }

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string searchText = txtBuscarEspecialidad.Text.Trim().ToLower();

            List<Especialidad> filteredList = _especialidadManager.ObtenerTodos()
                .Data
                .Where(Especialidad => Especialidad.Nombre.Trim().ToLower().Contains(searchText))
                .ToList();

            dgvEspecialidades.DataSource = filteredList;
            dgvEspecialidades.DataBind();
        }

        protected void dgvEspecialidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnModificar.Enabled = true;

            GridViewRow selectedRow = dgvEspecialidades.SelectedRow;

            if(selectedRow != null)
            {
                string cellValue = selectedRow.Cells[0].Text;
                string cellNombre = HttpUtility.HtmlDecode(selectedRow.Cells[1].Text);


                if(!string.IsNullOrEmpty(cellValue))
                {
                    txtEspecialidadSeleccionada.Text = cellNombre;
                }

                if(!string.IsNullOrEmpty(cellValue) && int.TryParse(cellValue.Trim(), out int idEspecialidad))
                {
                    Session["IdEspecialidad"] = idEspecialidad;

                    CargarEspecialidad(int.Parse(cellValue));
                }

            }
        }

        
        protected void CargarEspecialidad(int idEspecilidad)
        {
            Response<Especialidad> especialidad = new Response<Especialidad>();

            especialidad = _especialidadManager.ObtenerPorId(idEspecilidad);

            txtNombreEspecialidad.Text = especialidad.Data.Nombre;
        }
        
        
        //protected void btnModificar_Click(object sender, EventArgs e)
        //{
        //    int idEspecialidad = int.Parse(Session["Idespecialidad"].ToString());

        //    CargarEspecialidad(idEspecialidad);
        //}

        protected void btnModificarEspecialidad_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtNombreEspecialidad.Text))
            {

                Especialidad _especialidad = new Especialidad();
                
                try
                {
                    _especialidad.Id = int.Parse(Session["IdEspecialidad"].ToString());
                    _especialidad.Nombre = txtNombreEspecialidad.Text;

                    _especialidadManager.Update(_especialidad);

                    modificarEspecialidadSuccess.Visible = true;
                }
                catch (Exception ex)
                {
                    modificarEspecialidadFailure.Visible = true;
                    throw ex;
                }
            }
            else
            {
                modificarEspecialidadFailure.Visible= true;
            }

            ReloadEspecialidades();

        }

        protected void btnGuardarEspecialidad_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCrearNombreEspecialidad.Text))
            {

                Especialidad _especialidad = new Especialidad();

                try
                {   //No necesitamos el id por que es auto incremental

                    _especialidad.Nombre = txtCrearNombreEspecialidad.Text;

                    _especialidadManager.Crear(_especialidad);

                    crearEspecialidadSuccess.Visible = true;
                }
                catch (Exception ex)
                {
                    crearEspecialidadFailure.Visible = true;
                    throw ex;
                }
            }
            else
            {
                crearEspecialidadFailure.Visible = true;
            }

            ReloadEspecialidades();
        }

       
        protected void btnAtras_Click(object sender, EventArgs e)
        {
            Session["IdEspecialidad"] = null;
            //Session["NombreEspecialidad"] = null;

            Response.Redirect("~/Pages/Home.aspx", false);
        }

        protected void ReloadEspecialidades()
        {
            string script = $@"
               setTimeout(function() {{
            window.location.href = '/Pages/Especialidades.aspx';
               }},5000);";

            ClientScript.RegisterStartupScript(this.GetType(), "DelayedRedirect", script, true);
        }
    }
}