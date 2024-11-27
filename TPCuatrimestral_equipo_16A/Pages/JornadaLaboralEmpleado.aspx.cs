using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class JornadaLaboralEmpleado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                int id = int.Parse(Request.QueryString["id"]);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            Response.Redirect("Confirmacion.aspx");
        }
    }
}