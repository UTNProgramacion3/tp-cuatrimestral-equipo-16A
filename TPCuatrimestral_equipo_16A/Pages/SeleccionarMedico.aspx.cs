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

            txtbMedicoSeleccionado.Text = (string)Session["DatosMedico"];

            try
            {
                
                if (!IsPostBack)
                {
                    CargarMedicos();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CargarMedicos()
        {
            Response <List<MedicoDto>> listaMedicos = medicoManager.ObtenerTodos();


            try
            {
                dgvMedicos.DataSource = listaMedicos.Data;
                dgvMedicos.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        protected void FiltrarTabla()
        {
            GridViewRow selectedRow = dgvMedicos.SelectedRow;

            if (selectedRow != null)
            {
                string cellValue = selectedRow.Cells[0].Text;
                string cellNombre = HttpUtility.HtmlDecode(selectedRow.Cells[1].Text);
                string cellApellido = HttpUtility.HtmlDecode(selectedRow.Cells[2].Text);
                string cellEspecialidad = HttpUtility.HtmlDecode(selectedRow.Cells[3].Text);

                if (!string.IsNullOrEmpty(cellNombre) && !string.IsNullOrEmpty(cellApellido) && !string.IsNullOrEmpty(cellEspecialidad))
                {
                    string datosMedico = cellNombre + " " + cellApellido + " - Especialidad: " + cellEspecialidad;
                    txtbMedicoSeleccionado.Text = datosMedico;
                    Session["DatosMedico"] = datosMedico;
                    
                    Session["NombreMedico"] = cellNombre;
                    Session["ApellidoMedico"] = cellApellido;
                    Session["Especialidad"] = cellEspecialidad;
                }

                if (!string.IsNullOrEmpty(cellValue) && int.TryParse(cellValue.Trim(), out int idMedico))
                {
                    Session["IdMedico"] = idMedico;

                }
                else
                {
                    Response.Write("El valor seleccionado no es un ID válido.");
                }
            }
        }

        
        protected void dgvMedicos_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarTabla();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            
            txtbMedicoSeleccionado.Text = (string)Session["DatosMedico"];

            string searchText = txtBuscarEspecialidad.Text.Trim().ToLower();

            // Filtramos la lista de especialidades según el texto buscado
            List<MedicoDto> filteredList = medicoManager.ObtenerTodos()
                .Data // Usamos el Data de la respuesta
                .Where(MedicoDto => MedicoDto.Especialidad.Nombre.Trim().ToLower().Contains(searchText))
                .ToList();

            dgvMedicos.DataSource = filteredList;
            dgvMedicos.DataBind();
        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            txtbMedicoSeleccionado.Text = (string)Session["DatosMedico"];

            //int idMedico = (int)Session["IdMedico"];
            
            if(txtBuscarEspecialidad.Text != "")
            {
                txtBuscarEspecialidad.Text = "";
                CargarMedicos();
            }
            
        }


        protected void btnAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/SeleccionarSede.aspx", false);
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/SeleccionarFechaHora.aspx", false);
        }

    }
}