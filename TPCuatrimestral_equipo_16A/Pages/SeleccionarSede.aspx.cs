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
    public partial class SeleccionarSede : System.Web.UI.Page
    {
        private DBManager dbManager;
        private SedeManager sedeManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            dbManager = new DBManager();
            sedeManager = new SedeManager();

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

        protected void txtBuscarSede_TextChanged(object sender, EventArgs eventArgs)
        {
            string searchText = txtBuscarSede.Text.ToLower();

            List<SedeDto> filteredList = sedeManager.ObtenerTodos()
                .Data
                .Where(SedeDto => SedeDto.Sede.Nombre.ToLower().Contains(searchText))
                .ToList();
            
            dgvSedes.DataSource = filteredList;
            dgvSedes.DataBind();
        }
    }
}