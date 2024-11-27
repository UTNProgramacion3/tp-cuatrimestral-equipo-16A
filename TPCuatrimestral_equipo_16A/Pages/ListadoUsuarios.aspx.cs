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
        private List<UsuarioBasicoDto> _usuarios;

        public void InitDependencies()
        {
            IUnityContainer unityContainer;
            _usuarioManager = (IUsuarioManager)Global.Container.Resolve(typeof(IUsuarioManager));

            CargarUsuarios();
            _usuarios = _listadoUsuarios;
            gvUsuarios.DataSource = _usuarios;
            gvUsuarios.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitDependencies();
            }
        }

        protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rolSeleccionado = int.Parse(ddlRoles.SelectedValue);
            _usuarios = FiltrarUsuarios(rolSeleccionado);

            gvUsuarios.DataSource = _usuarios;
            gvUsuarios.DataBind();
        }

        private void CargarRoles()
        {
            var roles = _usuarioManager.ObtenerAllRoles();
            ddlRoles.Items.Clear();
            foreach (var rol in roles)
            {
                ddlRoles.Items.Add(new ListItem(rol.Nombre, rol.Id.ToString()));
            }
        }

        private void CargarUsuarios()
        {
            _listadoUsuarios = _usuarioManager.ObtenerUsuariosDataBasica();
        }

        private List<UsuarioBasicoDto> FiltrarUsuarios(int rolSeleccionado)
        {
            if (rolSeleccionado == 0)
            {
                return _listadoUsuarios;
            }

            return _listadoUsuarios.Where(u => u.RolId == rolSeleccionado).ToList();
        }
    }
}
