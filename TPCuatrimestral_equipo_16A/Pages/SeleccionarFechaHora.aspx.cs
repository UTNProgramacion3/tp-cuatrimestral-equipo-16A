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
					ObtenerTurnosDisponibles();
				}
			}
			catch (Exception ex)
			{

				throw ex;
			}
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