using Business.Dtos;
using Business.Interfaces;
using Business.Services;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class EdicionPaciente : System.Web.UI.Page
    {
        private IPacienteManager _pacienteManager;
        private IPersonaManager _personaManager;
        private Usuario _usuario;
        private PacienteSimpleDto _paciente;

        private void InitDependencies()
        {
            IUnityContainer unityContainer;
            _pacienteManager = (IPacienteManager)Global.Container.Resolve(typeof(IPacienteManager));
            _personaManager = (IPersonaManager)Global.Container.Resolve(typeof(IPersonaManager));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
                InitDependencies();
      
                if (Session["EdicionDePaciente"] != null && Session["UserLogueado"] != null)
                {
                    _usuario = (Usuario)Session["Userlogueado"];

                    if(_usuario.Rol.Id == (int) RolesEnum.Administrador || _usuario.Rol.Id == (int)RolesEnum.Recepcionista)
                    {
                        _paciente = (PacienteSimpleDto)Session["EdicionDePaciente"];
                        
                        if(!IsPostBack)
                        {

                            txtNombre.Text = _paciente.Nombre;
                            txtApellido.Text = _paciente.Apellido;
                            txtDocumento.Text = _paciente.Documento.ToString();
                            txtTelefono.Text = _paciente.Telefono;
                            txtFechaNacimiento.Text = _paciente.FechaNacimiento.ToString("yyyy-MM-dd");
                            txtEmail.Text = _paciente.EmailPersonal;
                            txtObraSocial.Text = _paciente.ObraSocial;
                            txtNroAfiliado.Text = _paciente.NroAfiliado;
                        }

                    }else
                    {
                        Response.Redirect("~/Pages/Home.aspx");
                    }
                }
            

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool tieneErrores = false;
            List<TextBox> textBoxes = new List<TextBox>
            {
                txtNombre,
                txtApellido,
                txtDocumento,
                txtTelefono,
                txtFechaNacimiento,
                txtEmail,
                txtObraSocial,
                txtNroAfiliado
            };

            foreach (var textBox in textBoxes)
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.BorderColor = Color.Red; 
                    tieneErrores = true;
                }
                else
                {
                    textBox.BorderColor = Color.Empty; 
                }
            }

            if (tieneErrores)
            {
                lblMensajeError.Text = "Todos los campos son obligatorios.";
                lblMensajeError.ForeColor = Color.Red;
                lblMensajeError.Visible = true;
                return;
            }

            lblMensajeError.Visible = false;

            if (_personaManager.EditarPersona(txtNombre.Text, txtApellido.Text, txtDocumento.Text, txtTelefono.Text, txtFechaNacimiento.Text, txtEmail.Text, _paciente.PersonaId))
            {
                if (_pacienteManager.EditarPaciente(txtObraSocial.Text, txtNroAfiliado.Text, _paciente.PersonaId))
                {
                    Response.Redirect("ListadoPacientes.aspx");
                }
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListadoPacientes.aspx");
        }
    }
}