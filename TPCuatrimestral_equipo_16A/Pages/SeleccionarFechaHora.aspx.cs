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
    public partial class SeleccionarFechaHora : System.Web.UI.Page

    {
		private DBManager dBManager;
		private TurnoManager turnoManager = new TurnoManager();
        protected void Page_Load(object sender, EventArgs e)
        {
			try
			{
				if(!IsPostBack)
				{
                    CompletarFechaHora();
				}
			}
			catch (Exception ex)
			{

				throw ex;
			}
        }


        protected void txtBoxFechaTurno_TextChanged(object sender, EventArgs e)
        {
            ObtenerTurnosDisponibles();
        }



        private void ObtenerTurnosDisponibles()
		{
			int idMedico = (int)Session["IdMedico"];

            Session["DiaTurno"] = txtBoxFechaTurno.Text;

            string fecha = (string)Session["DiaTurno"];

			
			DataTable table = new DataTable();

			table = turnoManager.ObtenerTurnosDisponibles(idMedico, fecha);

            dgvFechaHorario.DataSource = table;
			dgvFechaHorario.DataBind();

        }

        protected void dgvFechaHorario_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            CapturarDatosTurno();
            CompletarFechaHora();

        }



        private void CapturarDatosTurno()
        {
            GridViewRow selectedRow = dgvFechaHorario.SelectedRow;

            if(selectedRow != null)
            {
                Session["FechaTurno"] = txtBoxFechaTurno.Text;
                Session["HoraTurno"] = dgvFechaHorario.SelectedRow.Cells[0].Text;
            }

        }


        protected void CompletarFechaHora()
        {
            
            if(Session["FechaTurno"] != null && Session["HoraTurno"] != null)
            {
                string fecha = (string)Session["FechaTurno"];
                string hora = (string)Session["HoraTurno"];

                txtBoxFechaHora.Text = "Fecha Turno: " + fecha + " - Hora: " + hora;
            }
        }


        protected void btnAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/SeleccionarMedico.aspx", false);
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {



            Response.Redirect("~/Pages/ConfirmarTurno.aspx", false);
        }


    }
}