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
                    if(Session["ReprogramarTurno"] != null)
                    {
                        btnAtras.Visible = false;
                    }

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
			
            if(Session["ReprogramarTurno"] != null)
            {
                                
                int idMedicoReprogramar = int.Parse(Session["IdMedico"].ToString());

                string chequeoFecha = Session["DiaTurno"].ToString();

                Session["DiaTurno"] = txtBoxFechaTurno.Text;

                if(DateTime.Parse(chequeoFecha) >= DateTime.Now.Date && DateTime.Parse(txtBoxFechaTurno.Text) >= DateTime.Now.Date)
                {
                    Session["IdTurnoAreprogramar"] = 
                    Session["DiaTurno"] = txtBoxFechaTurno.Text;
                }
                else
                {
                    Session["DiaTurno"] = chequeoFecha;
                    return;
                }

                string fechaReprogramar = (string)Session["DiaTurno"];

                DataTable tableReprogramar = new DataTable();

                tableReprogramar = turnoManager.ObtenerTurnosDisponibles(idMedicoReprogramar, fechaReprogramar);

                dgvFechaHorario.DataSource = tableReprogramar;
                dgvFechaHorario.DataBind();
            }
            else
            {
                int idMedico = (int)Session["IdMedico"];

                Session["DiaTurno"] = txtBoxFechaTurno.Text;

                string chequeoFecha = txtBoxFechaTurno.Text;

                if (DateTime.Parse(chequeoFecha) < DateTime.Now.Date)
                {
                    
                    return;
                }
                
                else

                {
                    string fecha = (string)Session["DiaTurno"];


                    DataTable table = new DataTable();

                    table = turnoManager.ObtenerTurnosDisponibles(idMedico, fecha);

                    dgvFechaHorario.DataSource = table;
                    dgvFechaHorario.DataBind();
                }

            }

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