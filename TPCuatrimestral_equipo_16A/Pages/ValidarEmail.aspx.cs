using Business.Interfaces;
using DataAccess.Extensions;
using Domain.Entities;
using System;
using System.Web.UI;
using Unity;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class ValidarEmail : System.Web.UI.Page
    {
        private IUsuarioManager _usuarioManager;
        private IEmailManager _emailManager;
        private ISeguridadService _seguridadService;
        private Usuario _usuario;

        public void InitDependencies()
        {
            IUnityContainer unityContainer;
            _usuarioManager = (IUsuarioManager)Global.Container.Resolve(typeof(IUsuarioManager));
            _emailManager = (IEmailManager)Global.Container.Resolve(typeof(IEmailManager));
            _seguridadService = (ISeguridadService)Global.Container.Resolve(typeof(ISeguridadService));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitDependencies();
            string token = Request.QueryString["token"];

            if (!string.IsNullOrEmpty(token))
            {
                var usuarioValidado = _usuarioManager.ValidarToken(token);
                if(usuarioValidado.Email != null)
                {
                    _usuario = usuarioValidado;
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
            var updatedUser = _usuarioManager.ActivarUsuario(_usuario, nuevaContraseña);
            var res = _usuarioManager.Update(updatedUser);
            if (!res.Data)
            {
                throw new Exception("Error al validar email");
            }
            var token = Request.QueryString["token"];
            _seguridadService.InhabilitarToken(token);
            
            lblMensaje.Text = "Contraseña cambiada exitosamente.";
        }
    }
}
