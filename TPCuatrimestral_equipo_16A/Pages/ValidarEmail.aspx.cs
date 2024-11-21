using DataAccess.Extensions;
using System;
using System.Web.UI;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class ValidarEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string token = Request.QueryString["token"];
            string email;

            //if (!string.IsNullOrEmpty(token) && token.ValidarToken(out email))
            //{
            //    lblMensaje.Text = "Token válido. Ahora, por favor ingrese una nueva contraseña.";

            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarContraseña", "mostrarCampoContraseña();", true);
            //}
            //else
            //{
            //    lblMensaje.Text = "El token no es válido o ha expirado.";
            //}
        }

        protected void btnCambiarContraseña_Click(object sender, EventArgs e)
        {
            string nuevaContraseña = txtNuevaContraseña.Text;

           

            lblMensaje.Text = "Contraseña cambiada exitosamente.";
        }
    }
}
