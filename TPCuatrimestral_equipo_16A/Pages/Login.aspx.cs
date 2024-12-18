﻿using Business.Interfaces;
using Business.Managers;
using DataAccess;
using Domain.Entities;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;
using static TPCuatrimestral_equipo_16A.Global;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class Login : System.Web.UI.Page
    {

        private IPacienteManager _pacienteManager;
        private IEmpleadoManager _empleadoManager;
        private IMedicoManager _medicoManager;
        private IUsuarioManager _usuarioManager;
        private IPersonaManager _personaManager;
        private bool _isEditModeEnabled;
        private void InitDependencies()
        {
            IUnityContainer unityContainer;
            _pacienteManager = (IPacienteManager)Global.Container.Resolve(typeof(IPacienteManager));
            _empleadoManager = (IEmpleadoManager)Global.Container.Resolve(typeof(IEmpleadoManager));
            _medicoManager = (IMedicoManager)Global.Container.Resolve(typeof(IMedicoManager));
            _usuarioManager = (IUsuarioManager)Global.Container.Resolve(typeof(IUsuarioManager));
            _personaManager = (IPersonaManager)Global.Container.Resolve(typeof(IPersonaManager));

            string isEdit = Request.QueryString["mode"] ?? "";
            _isEditModeEnabled = isEdit != "" && isEdit.ToLower() == "edit";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            InitDependencies();
            GlobalData.UsuarioLogueado = null;
            if (!IsPostBack)
            {
                if (Session["UserLogueado"] != null)
                {
                    Response.Redirect("Home.aspx");
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario.Email = txtEmail.Text;
            usuario.Passwordhash = txtPassword.Text;

            var response = _usuarioManager.LogIn(usuario);

                if (response.Success)
                {
                GlobalData.UsuarioLogueado = response.Data;
                Response.Redirect("Home.aspx", false);

                }
                else
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = response.Message;
                }
        }
    }
}