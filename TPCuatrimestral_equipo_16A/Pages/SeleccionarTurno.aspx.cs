using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Business.Interfaces;
using Business.Managers;
using DataAccess;
using Domain.Entities;
using Domain.Response;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class SeleccionarMedico : System.Web.UI.Page
    {
        private DBManager dbManager;
        private EspecialidadManager especialidadManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            dbManager = new DBManager();
            Response<Especialidad> responseEspecialidad = new Response<Especialidad>();
            especialidadManager = new EspecialidadManager(dbManager, responseEspecialidad);

            try
            {
                
                if (!IsPostBack)
                {
                    CargarEspecialidades();
                    CargarMedicos();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarEspecialidades()
        {
            
            Response<List<Especialidad>> listaEspecialidad = especialidadManager.ObtenerTodos();

           
            if (listaEspecialidad != null && listaEspecialidad.Data != null)
            {
                ddlEspecialidades.DataSource = listaEspecialidad.Data;
                ddlEspecialidades.DataTextField = "Nombre";
                ddlEspecialidades.DataValueField = "Id";
                ddlEspecialidades.DataBind();
            }
            else
            {
                
                ddlEspecialidades.Items.Clear();
                ddlEspecialidades.Items.Add(new ListItem("No hay especialidades disponibles", "-1"));
            }
        }

        private void CargarMedicos()
        {
            dvgMedicos.DataSource = ObtenerDatos();
            dvgMedicos.DataBind();
        }

        protected DataTable ObtenerDatos()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Nombre");
            table.Columns.Add("Apellido");
            table.Columns.Add("Horario");

            table.Rows.Add("Pablo", "Perez", "14:00");
            table.Rows.Add("Pablo", "Perez", "14:30");
            table.Rows.Add("Pablo", "Perez", "15:00");

            return table;
        }

        protected void txtBuscarEspecialidad_TextChanged(object sender, EventArgs eventArgs)
        {
            string searchText = txtBuscarEspecialidad.Text.ToLower();

            // Filtramos la lista de especialidades según el texto buscado
            List<Especialidad> filteredList = especialidadManager.ObtenerTodos()
                .Data // Usamos el Data de la respuesta
                .Where(especialidad => especialidad.Nombre.ToLower().Contains(searchText))
                .ToList();

            ddlEspecialidades.DataSource = filteredList;
            ddlEspecialidades.DataTextField = "Nombre";
            ddlEspecialidades.DataValueField = "Id";
            ddlEspecialidades.DataBind();
        }
    }
}