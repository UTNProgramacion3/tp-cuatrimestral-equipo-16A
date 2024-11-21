using Business.Interfaces;
using DataAccess.Extensions;
using System;
using System.Web.UI;
using Unity;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class ValidarEmail : System.Web.UI.Page
    {
        private IUsuarioManager _usuarioManager;

        public void InitDependencies()
        {
            IUnityContainer unityContainer;
            _usuarioManager = (IUsuarioManager)Global.Container.Resolve(typeof(IUsuarioManager));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitDependencies();
            string token = Request.QueryString["token"];

            if (!string.IsNullOrEmpty(token))
            {
                var usuarioValidado = _usuarioManager.ValidarToken(token);

                if(usuarioValidado.Email != "")
                {
                    lblMensaje.Text = "Token válido. Ahora, por favor ingrese una nueva contraseña.";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "mostrarContraseña", "mostrarCampoContraseña();", true);
                }
            }
            else
            {
                lblMensaje.Text = "El token no es válido o ha expirado.";
            }
        }

        protected void btnCambiarContraseña_Click(object sender, EventArgs e)
        {
            string nuevaContraseña = txtNuevaContraseña.Text;

           

            lblMensaje.Text = "Contraseña cambiada exitosamente.";
        }
    }
}
