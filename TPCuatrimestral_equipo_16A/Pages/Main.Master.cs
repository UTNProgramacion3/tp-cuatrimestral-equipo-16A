using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_equipo_16A.Views
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected Usuario Usuario = new Usuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserLogueado"] != null)
            {
               
                pnlUserLogged.Visible = true;
                pnlLoginRegister.Visible = false;
                Usuario = (Usuario) Session["UserLogueado"];

               
                imgProfile.ImageUrl = Session["ImgPerfil"] != null ? "~/ImagesFolder/" + Session["ImgPerfil"].ToString() : "~/ImagesFolder/" + "default-profile.jpg";
            }
            else
            {
                
                pnlUserLogged.Visible = false;
                pnlLoginRegister.Visible = true;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["UserLogueado"] = null;
            Session["ImgPerfil"] = null;

            Response.Redirect("~/Pages/Home.aspx");
        }
    }
}