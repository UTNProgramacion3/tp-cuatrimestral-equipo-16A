using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Tracing;
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
    public partial class SeleccionarSede : System.Web.UI.Page
    {
        private DBManager dbManager;
        private SedeManager sedeManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            dbManager = new DBManager();
            sedeManager = new SedeManager();

            txtbSedeSeleccionada.Text = (string)Session["NombreSede"];

            if (!IsPostBack)
            {
                CargarSedes();
            }
        }

        private void CargarSedes()
        {
            Response<List<SedeDto>> listaSedes = sedeManager.ObtenerTodos();

            try
            {
                dgvSedes.DataSource = listaSedes.Data;
                dgvSedes.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void dgvSedes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow selectedRow = dgvSedes.SelectedRow;

            if (selectedRow != null)
            {
                string cellValue = selectedRow.Cells[0].Text;
                string cellNombre = HttpUtility.HtmlDecode(selectedRow.Cells[1].Text);

                if (!string.IsNullOrEmpty(cellNombre))
                {
                    txtbSedeSeleccionada.Text = cellNombre;
                    Session["NombreSede"] = cellNombre;
                }

                if (!string.IsNullOrEmpty(cellValue) && int.TryParse(cellValue.Trim(), out int idSede))
                {
                    Session["IdSede"] = idSede;
                }
                else
                {
                    Response.Write("El valor seleccionado no es un ID válido.");
                }
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            txtbSedeSeleccionada.Text = (string)Session["NombreSede"];

            string searchText = txtBuscarSede.Text.Trim().ToLower();

            List<SedeDto> filteredList = sedeManager.ObtenerTodos()
                .Data
                .Where(SedeDto => SedeDto.Sede.Nombre.Trim().ToLower().Contains(searchText))
                .ToList();

            dgvSedes.DataSource = filteredList;
            dgvSedes.DataBind();
        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            txtbSedeSeleccionada.Text = (string)Session["NombreSede"];

            //int idSede = (int)Session["IdSede"];

            if(txtBuscarSede.Text != "")
            {
                txtBuscarSede.Text = "";
                CargarSedes();
            }


        }

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/SeleccionarPaciente.aspx", false);
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/SeleccionarMedico.aspx", false);
        }

    }
}