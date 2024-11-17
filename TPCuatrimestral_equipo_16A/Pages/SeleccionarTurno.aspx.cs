using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Business.Dtos;
using Business.Interfaces;
using Business.Managers;
using DataAccess;
using Domain.Entities;
using Domain.Response;
using Utils;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class SeleccionarMedico : System.Web.UI.Page
    {
        private DBManager dbManager;
        private EspecialidadManager especialidadManager;
        private MedicoManager medicoManager;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            dbManager = new DBManager();
            Response<Especialidad> responseEspecialidad = new Response<Especialidad>();
            especialidadManager = new EspecialidadManager(dbManager, responseEspecialidad);
            medicoManager = new MedicoManager(dbManager, new Response<Medico>(), new Mapper<MedicoDto>());

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
            Response <List<MedicoDto>> listaMedicos = medicoManager.ObetenerTodos();


            try
            {
                dvgMedicos.DataSource = listaMedicos.Data;
                dvgMedicos.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
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