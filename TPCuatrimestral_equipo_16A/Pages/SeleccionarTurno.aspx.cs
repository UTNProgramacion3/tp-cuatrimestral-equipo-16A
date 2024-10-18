using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Business.Managers;
using Domain;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class SeleccionarMedico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                dvgMedicos.DataSource = ObtenerDatos();
                dvgMedicos.DataBind();
            }
        }

        protected DataTable ObtenerDatos()
        {
            DataTable table = new DataTable();

            table.Columns.Add("Nombre");
            table.Columns.Add("Apellido");
            table.Columns.Add("Horario");

            table.Rows.Add("Pablo", "Perez", "14:00");
            table.Rows.Add("Pablo", "Perez", "14:30");
            table.Rows.Add("Pablo", "Perez", "15:00");

            return table;
        }
    }
}