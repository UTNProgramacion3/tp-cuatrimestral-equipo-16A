using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class RegisterSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RegisterSuccess"] != null)
            {
                lblSuccessMessage.Text = Session["RegisterSuccess"].ToString();
            }else
            {
                Response.Redirect(ResolveUrl("~/Pages/Home.aspx"));
            }
        }
    }
}