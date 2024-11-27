﻿using Business.Managers;
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
using static TPCuatrimestral_equipo_16A.Global;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            UsuarioManager usuarioManager = new UsuarioManager();
            usuario.Email = txtEmail.Text;
            usuario.Passwordhash = txtPassword.Text;

            var response = usuarioManager.LogIn(usuario);

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