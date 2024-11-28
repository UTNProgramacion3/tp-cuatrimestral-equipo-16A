using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class CancelarTurno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Home.aspx", false);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Session["CancelarTurno"] = true;
            Response.Redirect("~/Pages/ListadoTurnos.aspx", false);
        }
    }
}