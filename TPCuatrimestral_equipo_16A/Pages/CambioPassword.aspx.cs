using Business.Interfaces;
using Business.Managers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;
using Utils;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class CambioPassword : System.Web.UI.Page
    {
        private Usuario _usuario;
        private IUsuarioManager _usuarioManager;

        private void InitDependencies()
        {
            IUnityContainer unityContainer;
            _usuarioManager = (IUsuarioManager)Global.Container.Resolve(typeof(IUsuarioManager));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserLogueado"] != null)
            {
                _usuario = (Usuario)Session["UserLogueado"];
                InitDependencies();
                pnlForm.Visible = true;

            } else
            {
                Response.Redirect("~/Pages/Home.aspx");
            }
        }

        private bool VerificarPassword()
        {
            string passActual = txtContraseñaActual.Text;

            if (!string.IsNullOrEmpty(passActual))
            {
                var res = PasswordHasher.VerifyPassword(passActual, _usuario.Passwordhash);
                return res;
            }
                return false;
        }

        private bool CambiarContraseña()
        {
            string passNueva = txtNuevaContraseña.Text;

            if(!string.IsNullOrEmpty(passNueva))
            {
                var res = _usuarioManager.CambiarPassword(passNueva, _usuario.Id);

                if(res.Success)
                {
                    return true;
                }
            }
            return false;
        }
        protected void btnCambiarContraseña_Click(object sender, EventArgs e)
        {
            if (VerificarPassword() == true)
            {
               var res = CambiarContraseña();

                if (res == true)
                {
                    pnlForm.Visible = false;
                    lblMensaje.Text = "Tu Contraseña fue cambiada con exito!";
                    lblMensaje.Visible = true;
                }else
                {
                    pnlForm.Visible = false;
                    lblMensaje.Text = "Error al intentar cambiar contraseña :(!";
                    lblMensaje.Visible = true;
                }

            }else
            {
                pnlForm.Visible = false;
                lblMensaje.Text = "Error, tu contraseña actual no coincide con la que ingresaste!";
                lblMensaje.Visible = true;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "mensajeRedireccion",
                "setTimeout(function(){ window.location.href = 'Home.aspx'; }, 3000);", true);
        }
    }
}