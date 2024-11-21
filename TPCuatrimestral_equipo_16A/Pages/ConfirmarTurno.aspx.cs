using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Dtos;
using DataAccess;
using Business.Managers;
using Domain.Enums;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class ConfirmarTurno : System.Web.UI.Page
    {   

        DBManager dbManager;
        TurnoManager turnoManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            dbManager = new DBManager();
            turnoManager = new TurnoManager();

            if (!IsPostBack)
            {
                ObtenerTurno();
            }
        }

        protected void ObtenerTurno()
        {
            lblNombrePaciente.Text = (string)Session["NombrePaciente"];
            lblApellidoPaciente.Text = (string)Session["ApellidoPaciente"];
            lblNombreMedico.Text = (string)Session["NombreMedico"];
            lblApellidoMedico.Text = (string)Session["ApellidoMedico"];
            lblEspecialidad.Text = (string)Session["Especialidad"];
            lblNombreSede.Text = (string)Session["NombreSede"];
            lblDireccionSede.Text = (string)Session["DireccionSede"];
            lblFecha.Text = (string)Session["FechaTurno"];
            lblHora.Text = (string)Session["HoraTurno"];

        }

        protected void CrearTurno()
        {
            TurnoDTO turno = new TurnoDTO();

            turno.Paciente.Id = (int)Session["IdPaciente"];
            turno.Medico.Id = (int)Session["IdMedico"];
            turno.Sede.Id = (int)Session["IdSede"];
            turno.EstadoTurno.Id = (int)Session["IdEstadoTurno"];
            turno.Turno.Fecha = DateTime.Parse((string)Session["FechaTurno"]);
            turno.Turno.Hora = TimeSpan.Parse((string)Session["HoraTurno"]);
            turno.Turno.Observaciones = (string)Session["Observaciones"];

            turnoManager.Crear(turno);

        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            btnConfirmar.Enabled = false;
            btnCancelar.Enabled = false;
            turnoSuccess.Visible = true;
            
            TurnoConfirmado();

            CrearTurno();

            Session.Clear();
            RedirectHome();

        }

        protected void TurnoConfirmado()
        {
            if(txtbObservaciones != null)
            {
                Session["Observaciones"] = txtbObservaciones.InnerText;
            }
            else
            {
                Session["Observaciones"] = "";
            }
            

            Session["IdEstadoTurno"] = (int)EstadosEnum.Confirmado;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            btnConfirmar.Enabled = false;
            btnCancelar.Enabled = false;
            turnoCancelado.Visible = true;

            Session.Clear();
            RedirectHome();
        }

        protected void RedirectHome()
        {
            string script = $@"
               setTimeout(function() {{
            window.location.href = '/Pages/Home.aspx';
               }},5000);";

            ClientScript.RegisterStartupScript(this.GetType(), "DelayedRedirect", script, true);
        }


        protected void btnAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/SeleccionarFechaHora.aspx", false);
        }
    }
}