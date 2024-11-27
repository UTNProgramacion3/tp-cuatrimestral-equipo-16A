using Business.Dtos;
using Business.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class ListadoUsuarios : System.Web.UI.Page
    {
        private IUsuarioManager _usuarioManager;
        private List<UsuarioBasicoDto> _listadoUsuarios;

        public void InitDependencies()
        {
            IUnityContainer unityContainer;
            _usuarioManager = (IUsuarioManager)Global.Container.Resolve(typeof(IUsuarioManager));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar todos los usuarios por defecto
            }
        }

        protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rolSeleccionado = int.Parse(ddlRoles.SelectedValue);
            //CargarUsuarios(rolSeleccionado);
        }

        private void CargarUsuarios()
        {
            var listadoUsuarios = _usuarioManager.ObtenerUsuariosDataBasica();
        }
    }
}
