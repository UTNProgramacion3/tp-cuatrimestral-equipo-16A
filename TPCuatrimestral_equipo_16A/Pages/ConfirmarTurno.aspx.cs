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
                DatosSesion();
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
            lblFecha.Text = ((DateTime)Session["FechaTurno"]).ToString("yyyy-MM-dd");
            lblHora.Text = ((TimeSpan)Session["HoraTurno"]).ToString(@"hh\:mm");

        }

        protected void CrearTurno()
        {
            TurnoDTO turno = new TurnoDTO();

            turno.Paciente.Id = (int)Session["IdPaciente"];
            turno.Medico.Id = (int)Session["IdMedico"];
            turno.Sede.Id = (int)Session["IdSede"];
            turno.EstadoTurno.Id = (int)Session["IdEstadoTurno"];
            turno.Turno.Fecha = (DateTime)Session["FechaTurno"];
            turno.Turno.Hora = (TimeSpan)Session["HoraTurno"];
            turno.Turno.Observaciones = (string)Session["Observaciones"];

            turnoManager.Crear(turno);

        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            btnConfirmar.Enabled = false;
            btnCancelar.Enabled = false;
            turnoCancelado.Visible = true;

            Session.Clear();
            RedirectHome();
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            btnConfirmar.Enabled = false;
            btnCancelar.Enabled = false;
            turnoSuccess.Visible = true;

            CrearTurno();

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


        protected void DatosSesion()
        {
            // Información del paciente
            Session["IdPaciente"] = 1;
            Session["NombrePaciente"] = "Juan";
            Session["ApellidoPaciente"] = "Pérez";

            // Información del médico
            Session["IdMedico"] = 2;
            Session["NombreMedico"] = "María";
            Session["ApellidoMedico"] = "González";

            // Información de la sede
            Session["IdSede"] = 1;
            Session["NombreSede"] = "Sede Central";
            Session["DireccionSede"] = "Av. Libertador 1000, Buenos Aires, Buenos Aires, 1000";

            // Información del turno
            Session["IdEstadoTurno"] = 1; // Pendiente
            Session["Especialidad"] = "Neurología";
            Session["Observaciones"] = "Ninguna";
            Session["FechaTurno"] = DateTime.Parse("2024-11-20");
            Session["HoraTurno"] = TimeSpan.Parse("10:00");
        }

    }
}