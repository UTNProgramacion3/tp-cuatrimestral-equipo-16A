using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_equipo_16A.Views
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //TurnosManager _manager = new TurnosManager();

                //dvgTurno.DataSource = _manager.ObtenerTodos();

                dgvTurnos.DataSource = ObtenerDatos();
                dgvTurnos.DataBind();
            }
        }

        protected DataTable ObtenerDatos()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("NombrePaciente");
            dt.Columns.Add("ApellidoPaciente");
            dt.Columns.Add("DniPaciente");
            dt.Columns.Add("NombreMedico");
            dt.Columns.Add("ApellidoMedico");
            dt.Columns.Add("Especialidad");
            dt.Columns.Add("Fecha");
            dt.Columns.Add("Horario");

            dt.Rows.Add("Juan", "Perez", "654321", "Lucas", "Gonzalez", "Cirujano", "23/10/24", "14:00");
            dt.Rows.Add("Guillermo", "Mendez", "123546", "Pablo", "Peralta", "Otorrino", "23/10/24", "14:00");
            dt.Rows.Add("Sebastian", "Romero", "654879", "Miguel", "Lozano", "Odontologo", "23/10/24", "15:00");


            return dt;
        }

     
    }
}