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
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

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

            if (pnlAgregarPermiso != null && txtNuevoPermiso != null && btnAgregarPermiso != null)
            {
                string nuevoPermiso = txtNuevoPermiso.Text;
                int moduloId = Convert.ToInt32(btnAgregarPermiso.CommandArgument);

                if (string.IsNullOrWhiteSpace(nuevoPermiso))
                {
                    txtNuevoPermiso.BorderColor = System.Drawing.Color.Red;

                    Label lblError = (Label)item.FindControl("lblError");
                    if (lblError != null)
                    {
                        lblError.Text = "Debes ponerle un nombre al permiso";
                        lblError.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    // Si el TextBox no está vacío, proceder a agregar el permiso
                    if (_seguridadService.AgregarPermiso(nuevoPermiso, moduloId))
                    {
                        // Si el permiso se agrega correctamente, recargar el Repeater
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
            Response.Redirect("RolesPermisos.aspx");
        }

        protected void rptPermisos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                var lblPermiso = (Label)e.Item.FindControl("lblPermiso");
                var txtPermiso = (TextBox)e.Item.FindControl("txtPermiso");
                var btnEditar = (Button)e.Item.FindControl("btnEditar");
                var btnCancelar = (Button)e.Item.FindControl("btnCancelar");

                lblPermiso.Visible = false;
                txtPermiso.Visible = true;
                txtPermiso.Text = lblPermiso.Text;
                btnEditar.Text = "Listo";
                btnEditar.CommandName = "Guardar";
                btnCancelar.Visible = true;
                txtPermiso.BorderColor = System.Drawing.Color.Blue;
            }
            else if (e.CommandName == "Guardar")
            {
                var txtPermiso = (TextBox)e.Item.FindControl("txtPermiso");
                var lblPermiso = (Label)e.Item.FindControl("lblPermiso");
                var btnEditar = (Button)e.Item.FindControl("btnEditar");
                var btnCancelar = (Button)e.Item.FindControl("btnCancelar");
                var permisoId = Convert.ToInt32(e.CommandArgument);
                var nuevoNombre = txtPermiso.Text;

                if (string.IsNullOrWhiteSpace(nuevoNombre))
                {
                    txtPermiso.BorderColor = System.Drawing.Color.Red;

                    Label lblError = (Label)e.Item.FindControl("lblError");
                    if (lblError != null)
                    {
                        lblError.Text = "Debes poner un nombre para el permiso";
                        lblError.ForeColor = System.Drawing.Color.Red;
                        lblError.Visible = true;
                    }

                    return; 
                }
                else
                {
                    _seguridadService.EditarPermiso(permisoId, nuevoNombre);

                    lblPermiso.Text = nuevoNombre;
                    lblPermiso.Visible = true;
                    txtPermiso.Visible = false;

                    btnEditar.Text = "Editar";
                    btnEditar.CommandName = "Editar";

                    btnCancelar.Visible = false;

                    Label lblError = (Label)e.Item.FindControl("lblError");
                    if (lblError != null)
                    {
                        lblError.Visible = false;
                    }
                }
            }
            else if (e.CommandName == "Cancelar")
            {
                var lblPermiso = (Label)e.Item.FindControl("lblPermiso");
                var txtPermiso = (TextBox)e.Item.FindControl("txtPermiso");
                var btnEditar = (Button)e.Item.FindControl("btnEditar");
                var btnCancelar = (Button)e.Item.FindControl("btnCancelar");

                lblPermiso.Visible = true;
                txtPermiso.Visible = false;
                btnEditar.Text = "Editar";
                btnEditar.CommandName = "Editar";
                btnCancelar.Visible = false;
            }
        }


        protected void btnTogglePermisos_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string moduloId = btn.CommandArgument;

            RepeaterItem item = (RepeaterItem)btn.NamingContainer;
            Panel permisosPanel = (Panel)item.FindControl("pnlPermisos");

            if (permisosPanel != null)
            {
                
                permisosPanel.Visible = !permisosPanel.Visible;
                btn.Text = permisosPanel.Visible ? "Ocultar Permisos" : "Mostrar Permisos";
            }
        }
    }
}