using System;
using Domain.Entities;
using Domain.Enums;
using Domain.Response;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Business.Interfaces;
using Business.Managers;
using Unity;
using System.Runtime.CompilerServices;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class AdmPermisos : System.Web.UI.Page
    {
        private ISeguridadService _seguridadService;

        private void InitDependencies()
        {
            IUnityContainer unityContainer;
            _seguridadService = (ISeguridadService)Global.Container.Resolve(typeof(ISeguridadService));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            InitDependencies();

            if (Session["UserLogueado"] != null)
            {
                Usuario user = (Usuario)Session["UserLogueado"];

                if (user.Rol.Id != (int)RolesEnum.Administrador)
                {
                    Response.Redirect("~/Pages/Home.aspx");
                }
                else if(!IsPostBack)  
                {
                    CargarRepeater();

                }
            }
            else
            {
                Response.Redirect("~/Pages/Home.aspx");
            }
        }

        private void CargarRepeater()
        {
            var res = _seguridadService.GetModulos();

            if (res.Success)
            {
                rptModulos.DataSource = res.Data;
                rptModulos.DataBind();

            }
        }


        protected void rptModulos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Obtenemos el modulo actual
                var modulo = (Modulo)e.Item.DataItem;

                // Encontramos el repetear anidado a rptModulos
                var rptPermisos = (Repeater)e.Item.FindControl("rptPermisos");

                // Enlazamos los permisos del modulo al repeater anidado
                if (rptPermisos != null && modulo.Permisos != null)
                {
                    rptPermisos.DataSource = modulo.Permisos;
                    rptPermisos.DataBind();
                }
            }
        }

        protected void btnGuardarPermiso_Click(object sender, EventArgs e)
        {
            Button btnGuardar = (Button)sender;
            RepeaterItem item = (RepeaterItem)btnGuardar.NamingContainer;

            Panel pnlAgregarPermiso = (Panel)item.FindControl("pnlAgregarPermiso");
            TextBox txtNuevoPermiso = (TextBox)item.FindControl("txtNuevoPermiso");
            Button btnAgregarPermiso = (Button)item.FindControl("btnAgregarPermiso");

            if(pnlAgregarPermiso != null && txtNuevoPermiso != null && btnAgregarPermiso != null)
            {
                string nuevoPermiso = txtNuevoPermiso.Text;
                int moduloId = Convert.ToInt32(btnAgregarPermiso.CommandArgument);
                if (!string.IsNullOrEmpty(nuevoPermiso))
                {
                    if(_seguridadService.AgregarPermiso(nuevoPermiso,moduloId))
                    {
                        CargarRepeater();
                    }
                }
            }
        }

        protected void btnCancelarPermiso_Click(object sender, EventArgs e)
        {
            Button btnCancelar = (Button)sender;
            RepeaterItem item = (RepeaterItem)btnCancelar.NamingContainer;

            Panel pnlAgregarPermiso = (Panel)item.FindControl("pnlAgregarPermiso");
            Button btnAgregarPermiso = (Button)item.FindControl("btnAgregarPermiso");

            if (pnlAgregarPermiso != null && btnAgregarPermiso != null)
            {
                pnlAgregarPermiso.Visible = false;
                btnAgregarPermiso.Visible = true;
            }
        }

        protected void btnAgregarPermiso_Click(object sender, EventArgs e)
        {
            Button btnAgregar = (Button)sender;
            RepeaterItem item = (RepeaterItem)btnAgregar.NamingContainer;

            Panel pnlAgregarPermiso = (Panel)item.FindControl("pnlAgregarPermiso");
            Button btnAgregarPermiso = (Button)item.FindControl("btnAgregarPermiso");

            if(pnlAgregarPermiso != null && btnAgregarPermiso != null)
            {
                pnlAgregarPermiso.Visible = true;
                btnAgregarPermiso.Visible = false;
            }

        }

        protected void btnPermisosPorRol_Click(object sender, EventArgs e)
        {
            Response.Redirect("PermisosPorRol.aspx");
        }
    }
}