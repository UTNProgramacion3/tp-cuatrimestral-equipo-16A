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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserLogueado"] != null)
            {
                // Usuario logueado, muestra la imagen de perfil y el botón de logout
                pnlUserLogged.Visible = true;
                pnlLoginRegister.Visible = false;

                // Asigna la URL de la imagen de perfil (puedes modificar para obtener la ruta real desde el usuario logueado)
                imgProfile.ImageUrl = Session["ImgPerfil"] != null ? "~/ImagesFolder/" + Session["ImgPerfil"].ToString() : "~/ImagesFolder/" + "default-profile.jpg";
            }
            else
            {
                // Usuario no logueado, muestra los botones de login y registro
                pnlUserLogged.Visible = false;
                pnlLoginRegister.Visible = true;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["UserLogueado"] = null;
            Session["ImgPerfil"] = null;

            // Redirige a la página de inicio (recargar la página también funciona)
            Response.Redirect("~/Pages/Home.aspx");
        }
    }
}