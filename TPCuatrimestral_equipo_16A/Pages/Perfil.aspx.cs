using Business.Interfaces;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace TPCuatrimestral_equipo_16A.Views
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        private IPersonaManager _personaManager;
        private IUsuarioManager _usuarioManager;
        private Usuario _usuario;

        private void InitDependencies()
        {
            IUnityContainer unityContainer;
            _personaManager = (IPersonaManager)Global.Container.Resolve(typeof(IPersonaManager));
            _usuarioManager = (IUsuarioManager)Global.Container.Resolve(typeof (IUsuarioManager));
        }

        private void VerificarRol(RolesEnum rol)
        {
            if (RolesEnum.Administrador == rol)
            {
                btnVerTurnos.Visible = false;
                btnCambiarPass.Visible = true;
                btnEditar.Visible = true;
            }
            else if (RolesEnum.Medico == rol || RolesEnum.Paciente == rol)
            {
                btnVerTurnos.Visible = true;
                btnCambiarPass.Visible = true;
                btnEditar.Visible = false;
            }
            else
            {
                btnVerTurnos.Visible = false;
                btnCambiarPass.Visible = true;
                btnEditar.Visible = false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            InitDependencies();
            if(!IsPostBack)
            {
                if (Session["UserLogueado"] != null)
                {
                    _usuario = (Usuario)Session["UserLogueado"];
                    VerificarRol((RolesEnum)_usuario.Rol.Id);
                    var res = _personaManager.ObtenerPorUsuario(_usuario.Id);

                    if (res.Success) 
                    {
                        lblNombreApellido.Text = res.Data.Nombre + " " + res.Data.Apellido;
                        lblEmail.Text = res.Data.EmailPersonal;
                        lblTelefono.Text = res.Data.Telefono;
                        lblFechaNacimiento.Text = res.Data.FechaNacimiento.ToString("yyyy-MM-dd");

                        if (_usuario.ImagenPerfil != null)
                        {
                            imgPerfil.ImageUrl = _usuario.ImagenPerfil;
                        }else
                        {
                            CargarImagenPerfil();
                        }
                    }

                } else
                {
                    Response.Redirect("~/Pages/Home.aspx");
                }

            }

        }

        private void CargarImagenPerfil()
        {
            string email = lblEmail.Text;

            string[] extensionesPermitidas = { ".jpg", ".jpeg", ".png", ".gif" };

            string imagenPath = null;

            foreach (string extension in extensionesPermitidas)
            {
                imagenPath = Server.MapPath($"~/Images/{email}{extension}");

                if (File.Exists(imagenPath))
                {
                    imgPerfil.ImageUrl = $"~/Images/{email}{extension}";
                    return; 
                }
            }

            imgPerfil.ImageUrl = "~/ImagesFolder/default-profile.jpg";



        }

        protected void btnSubirFoto_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                string email = lblEmail.Text;

                if (!string.IsNullOrEmpty(email))
                {
                    string extension = Path.GetExtension(fileUpload.PostedFile.FileName).ToLower();

                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif")
                    {
                        // Construye la ruta del archivo utilizando el correo electrónico
                        string fileName = email + extension;  // El nombre de la imagen será el correo electrónico + la extensión
                        string filePath = Server.MapPath("~/UploadedImages/" + fileName);

                        // Si el archivo ya existe, se reemplaza
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);  
                        }

                        // Guarda el nuevo archivo
                        fileUpload.SaveAs(filePath);

                        // Actualiza la imagen del perfil en la página
                        imgPerfil.ImageUrl = "~/UploadedImages/" + fileName;

                        // Guardamos la direccion en db y en session.
                        _usuario = (Usuario)Session["UserLogueado"];
                        _usuario.ImagenPerfil = imgPerfil.ImageUrl;
                        _usuarioManager.Update(_usuario);
                        Session["UserLogueado"] = _usuario;
                    }
                }
            }
        }
    }
}