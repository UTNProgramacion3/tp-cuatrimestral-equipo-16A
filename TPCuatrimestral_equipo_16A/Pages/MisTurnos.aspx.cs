using Business.Dtos;
using Business.Interfaces;
using Business.Managers;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class MisTurnos : System.Web.UI.Page
    {
        private ITurnoManager _turnoManager;
        private IUsuarioManager _usuarioManager;
        private Usuario _usuario;

        private void InitDependencies()
        {
            IUnityContainer unityContainer;
            _turnoManager = (ITurnoManager)Global.Container.Resolve(typeof(ITurnoManager));
            _usuarioManager = (IUsuarioManager)Global.Container.Resolve(typeof(IUsuarioManager));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            InitDependencies();

            if (Session["UserLogueado"] != null)
            {
                _usuario = (Usuario)Session["UserLogueado"];

                if(!IsPostBack)
                {

                    if (_usuario.Rol.Id == (int)RolesEnum.Medico)
                    {
                        CargarTurnosMedico();

                    }else if(_usuario.Rol.Id == (int)RolesEnum.Paciente)
                    {
                        CargarTurnosPaciente();
                    }
                    else
                    {
                        Response.Redirect("~/Pages/Home.aspx");
                    }
                }
            }
            else
            {
                Response.Redirect("~/Pages/Home.aspx");
            }

        }

        private void CargarTurnosMedico()
        {
            var medicoId = _usuarioManager.ObtenerMedicoPorUsuario(_usuario.Id);

            if (medicoId > 0)
            {
                var response = _turnoManager.ObtenerTurnosPorMedico(medicoId);

                if (response.Success)
                {
                    gvTurnos.DataSource = response.Data;
                    gvTurnos.DataBind();
                }
            }
        }

        private void CargarTurnosPaciente()
        {
            var pacienteId = _usuarioManager.ObtenerPacientePorUsuario(_usuario.Id);

            if (pacienteId > 0)
            {
                var response = _turnoManager.ObtenerTurnosPorPaciente(pacienteId);

                if (response.Success)
                {
                    gvTurnos.DataSource = response.Data;
                    gvTurnos.DataBind();
                }
            }
        }


        protected void gvTurnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Comentar")
            {
                int turnoId = Convert.ToInt32(e.CommandArgument);

                ViewState["TurnoId"] = turnoId;

                pnlComentario.Visible = true;

                string comentario = _turnoManager.ObtenerComentario(turnoId);

                txtComentario.Text = comentario;

                if (_usuario.Rol.Id == (int)RolesEnum.Paciente)
                    txtComentario.Enabled = false;

            }
        }
        private void GuardarComentario()
        {
            if (string.IsNullOrWhiteSpace(txtComentario.Text))
            {
                txtComentario.BorderColor = System.Drawing.Color.Red;

                lblErrorComentario.Text = "El comentario no puede estar vacío.";
                lblErrorComentario.Visible = true;

                return; // Salir de la función para evitar que se guarde el comentario
            }

            txtComentario.BorderColor = System.Drawing.Color.Empty;
            lblErrorComentario.Visible = false;

            if (ViewState["TurnoId"] != null)
            {
                int turnoId = (int)ViewState["TurnoId"];
                string comentario = txtComentario.Text;

                var response = _turnoManager.GuardarComentario(turnoId, comentario);

                if (response.Success)
                {
                    pnlComentario.Visible = false;
                    txtComentario.Text = string.Empty;
                    CargarTurnosMedico();
                }
                else
                {
                    lblErrorComentario.Text = "Ocurrió un error al guardar el comentario.";
                    lblErrorComentario.Visible = true;
                }
            }
        }

        protected void btnCerrarPanel_Click(object sender, EventArgs e)
        {
            pnlComentario.Visible = false;
            txtComentario.Text = string.Empty;
            txtComentario.BorderColor = System.Drawing.Color.Empty;
            lblErrorComentario.Visible = false;
        }

        protected void btnGuardarComentario_Click(object sender, EventArgs e)
        {
            GuardarComentario();
        }

        protected void btnCancelarComentario_Click(object sender, EventArgs e)
        {
            pnlComentario.Visible = false;
            txtComentario.Text = string.Empty;
            txtComentario.BorderColor = System.Drawing.Color.Empty;
            lblErrorComentario.Visible = false;
        }

        protected void gvTurnos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (_usuario.Rol.Id == (int)RolesEnum.Paciente)
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Button btnComentar = (Button)e.Row.FindControl("btnComentar");
                    if (btnComentar != null)
                    {
                        btnComentar.Visible = false;
                    }
                }
            }
        }
    }

}