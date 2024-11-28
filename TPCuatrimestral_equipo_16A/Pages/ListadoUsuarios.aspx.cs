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
using static TPCuatrimestral_equipo_16A.Global;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class ListadoUsuarios : System.Web.UI.Page
    {
        private IUsuarioManager _usuarioManager;
        private List<UsuarioBasicoDto> _listadoUsuarios = new List<UsuarioBasicoDto>();
        private List<UsuarioBasicoDto> _usuarios;

        public void InitDependencies()
        {
            IUnityContainer unityContainer;
            _usuarioManager = (IUsuarioManager)Global.Container.Resolve(typeof(IUsuarioManager));
            Usuario user = GlobalData.UsuarioLogueado;
            CargarUsuarios();
            CargarRoles();
            Session["ListadoUsuarios"] = _listadoUsuarios;
            Session["Usuarios"] = _listadoUsuarios;
            gvUsuarios.DataSource = _usuarios;
            gvUsuarios.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitDependencies();
            }
            else
            {
                _listadoUsuarios = (List<UsuarioBasicoDto>)Session["ListadoUsuarios"];
                _usuarios = (List<UsuarioBasicoDto>)Session["Usuarios"];
                gvUsuarios.DataSource = _usuarios;
                gvUsuarios.DataBind();
            }
        }

        protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rolSeleccionado = int.Parse(ddlRoles.SelectedValue);
            _usuarios = FiltrarUsuarios(rolSeleccionado);
            Session["Usuarios"] = _usuarios;
            gvUsuarios.DataSource = _usuarios;
            gvUsuarios.DataBind();
        }

        private void CargarRoles()
        {
            var roles = _usuarioManager.ObtenerAllRoles();
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
            var listado = _listadoUsuarios.Where(u => u.RolId == rolSeleccionado).ToList();
            return listado;
        }

        protected void gvUsuarios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Configura el GridView para el modo de edición
            gvUsuarios.EditIndex = e.NewEditIndex;

            // Vuelve a enlazar los datos al GridView
            BindGrid();
        }

        protected void gvUsuarios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // Cancela la edición y vuelve a la vista normal
            gvUsuarios.EditIndex = -1;
            BindGrid();
        }

        protected void gvUsuarios_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Aquí se pueden obtener los datos actualizados y guardar los cambios
            int userId = Convert.ToInt32(gvUsuarios.DataKeys[e.RowIndex].Value);
            string nuevoValor = ((TextBox)gvUsuarios.Rows[e.RowIndex].FindControl("txtNombre")).Text;

            // Lógica para actualizar el usuario en la base de datos
            // _usuarioManager.ActualizarUsuario(userId, nuevoValor);

            gvUsuarios.EditIndex = -1;
            BindGrid();
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int userId = Convert.ToInt32(e.CommandArgument); // Obtiene el ID del usuario

            switch (e.CommandName)
            {
                case "Edit":
                    // Redirige a la página de edición con el modo "edit" y el ID del usuario
                    Response.Redirect($"CrearNuevoUsuario.aspx?Mode=edit&Id={userId}");
                    break;

                case "View":
                    // Redirige a la página de vista con el ID del usuario
                    Response.Redirect($"VerUsuario.aspx?Id={userId}");
                    break;

                case "Delete":
                    // Lógica para eliminar el usuario
                    EliminarUsuario(userId);
                    break;
            }
        }


        private void EliminarUsuario(int userId)
        {
            //_usuarioManager.EliminarUsuario(userId);
            CargarUsuarios();
            gvUsuarios.DataSource = _listadoUsuarios;
            gvUsuarios.DataBind();
        }

        private void BindGrid()
        {
            gvUsuarios.DataSource = _usuarios;
            gvUsuarios.DataBind();
        }
    }
}
