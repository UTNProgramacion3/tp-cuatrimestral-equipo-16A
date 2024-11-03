using System;
using System.Web;
using System.Web.UI;
using Business.Managers;
using DataAccess;
using Domain.Entities;
using Domain.Response;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class UserRegister : System.Web.UI.Page
    {
        protected UsuarioManager _usuarioManager = new UsuarioManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                pnlPersonaCreate.Visible = false;
                btnRegister.Enabled = false;
            }
        }

        protected void ShowMessage(string message)
        {
            lblMensaje.Text = message;
            lblMensaje.Visible = true;

        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(txtEmail.Text) && !string.IsNullOrEmpty(txtPassword.Text))
            {
                pnlPersonaCreate.Visible = true;
                btnNext.Enabled = false;
                btnNext.Visible = false;
                lblMensaje.Visible = false;
            }
            else
            {
                ShowMessage("Debe completar los campos obligatorios de usuario.");
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Usuario user = new Usuario();
            Response<Usuario> response;
            
            if(!_usuarioManager.ExisteMail(txtEmail.Text))
            {
                user.Email = txtEmail.Text;
                user.Passwordhash = txtPassword.Text;
                user.Rol.Id = 2;
                user.Activo = true;
                response = _usuarioManager.Crear(user);

                if(response.Success == true)
                {
                    Session.Add("RegisterSuccess", response.Message);
                    Session.Add("UserLogueado", response.Data);
                    Response.Redirect(ResolveUrl("~/Pages/RegisterSuccess.aspx"), false);
                }else
                {
                    lblMensaje.Text = response.Message;
                    lblMensaje.Visible = true;
                }
                    
            }else
            {
                lblMensaje.Text = "El email ingresado ya esta registrado!";
                lblMensaje.Visible = true;
                txtEmail.BorderColor = System.Drawing.Color.Red;
            }

                

        }
    }
}