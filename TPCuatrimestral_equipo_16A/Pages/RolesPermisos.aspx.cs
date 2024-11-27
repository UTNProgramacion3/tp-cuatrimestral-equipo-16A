using Business.Interfaces;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class RolesPermisos : System.Web.UI.Page
    {
        private ISeguridadService _seguridadService;
        private List<Rol> _roles;
        protected List<Permiso> _AllPermisos;

        private void InitDependencies()
        {
            IUnityContainer unityContainer;
            _seguridadService = (ISeguridadService)Global.Container.Resolve(typeof(ISeguridadService));
        }
        private void CargarRoles()
        {
            var res = _seguridadService.GetRolesConPermisos();

            if (res.Success)
            {
                _roles = res.Data;
                rptRoles.DataSource = _roles;
                rptRoles.DataBind();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            InitDependencies();
            

            if (Session["UserLogueado"] != null)
            {
                Usuario user = (Usuario)Session["UserLogueado"];
                var res = _seguridadService.GetPermisos();
                _AllPermisos = res.Data;
                if (user.Rol.Id != (int)RolesEnum.Administrador)
                {
                    Response.Redirect("~/Pages/Home.aspx");
                }
                else if (!IsPostBack)
                {

                    CargarRoles();

                }
            }
            else
            {
                Response.Redirect("~/Pages/Home.aspx");
            }

        }

        protected List<Permiso> GetPermisosDisponibles(int rolId)
        {
            var todosLosPermisos = _AllPermisos; 
            var permisosDelRol = _seguridadService.GetPermisosPorRol(rolId);

            var permisosDisponibles = new List<Permiso>();

            foreach (var permiso in todosLosPermisos)
            {
                bool estaEnRol = false;

                foreach (var permisoDelRol in permisosDelRol.Data)
                {
                    if (permiso.Id == permisoDelRol.Id)
                    {
                        estaEnRol = true;
                        break; 
                    }
                }

                if (!estaEnRol)
                {
                    permisosDisponibles.Add(permiso);
                }
            }

            return permisosDisponibles;

        }
        protected void rptRoles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int rolId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "AgregarPermiso")
            {
                // Encuentra el DropDownList dentro del ItemTemplate
                var ddlPermisos = (DropDownList)e.Item.FindControl("ddlPermisosDisponibles");

                if (ddlPermisos != null && !string.IsNullOrEmpty(ddlPermisos.SelectedValue) && ddlPermisos.SelectedValue != "0")
                {
                    int permisoId = Convert.ToInt32(ddlPermisos.SelectedValue);

                    // Llama al método para agregar el permiso al rol
                    AgregarPermisoARol(rolId, permisoId);

                    // Recarga los roles y permisos para reflejar los cambios
                    CargarRoles();
                }
            }
        }
        private void AgregarPermisoARol(int rolId, int permisoId)
        {
            _seguridadService.AsignarPermisoARol(permisoId, rolId);
        }

        private void EliminarPermisoDeRol(int rolId, int permisoId)
        {
            _seguridadService.EliminarPermisoDeRol(permisoId, rolId);
        }

        protected void rptRoles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var rol = (Rol)e.Item.DataItem;
                var ddlPermisos = (DropDownList)e.Item.FindControl("ddlPermisosDisponibles");
                if (ddlPermisos != null)
                {
                    var permisosDisponibles = GetPermisosDisponibles(rol.Id);

                    ddlPermisos.DataSource = permisosDisponibles;
                    ddlPermisos.DataTextField = "Nombre";
                    ddlPermisos.DataValueField = "Id";
                    ddlPermisos.DataBind();

                    ddlPermisos.Items.Insert(0, new ListItem("-- Seleccione un permiso --", ""));
                }
            }
        }

        protected void btnEliminarPermiso_Click(object sender, EventArgs e)
        {
            var btnEliminar = sender as Button;

            if (btnEliminar != null)
            {
                var repeaterItem = btnEliminar.NamingContainer as RepeaterItem;
                if (repeaterItem != null)
                {

                    var hfPermisoId = repeaterItem.FindControl("hfPermisoId") as HiddenField;
                    int permisoId = hfPermisoId != null ? Convert.ToInt32(hfPermisoId.Value) : 0;

                    var card = repeaterItem.NamingContainer.NamingContainer as RepeaterItem;
                    if (card != null)
                    {
                        var hfRolId = card.FindControl("hfRolId") as HiddenField;
                        int rolId = hfRolId != null ? Convert.ToInt32(hfRolId.Value) : 0;

                        EliminarPermisoDeRol(rolId, permisoId);
                    }
                }
            }

            CargarRoles();
        }
    }
}