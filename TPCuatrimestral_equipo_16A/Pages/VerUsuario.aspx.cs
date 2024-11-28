using Business.Dtos;
using Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class VerUsuario : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                int userId;
                if (int.TryParse(Request.QueryString["Id"], out userId))
                {
                    CargarDatosUsuario(userId);
                }
                else
                {
                    MostrarMensajeError("ID de usuario inválido.");
                }
            }
        }

        private void CargarDatosUsuario(int userId)
        {
            try
            {
                //// Aquí debes conectar con tu capa de negocio o datos para obtener el usuario
                //UsuarioBasicoDto usuario = _usuarioManager.ObtenerUsuariosDataBasica(userId); // Supongamos que este método trae los datos desde la base de datos.

                //if (usuario != null)
                //{
                //    lblNombre.Text = usuario.Nombre;
                //    lblApellido.Text = usuario.Apellido;
                //    lblRol.Text = usuario.Rol;
                //    lblDocumento.Text = usuario.Documento.ToString();
                //    lblEmailPersonal.Text = usuario.EmailPersonal;
                //    lblTelefono.Text = usuario.Telefono;
                //    lblFechaNacimiento.Text = usuario.FechaNacimiento.ToString("dd/MM/yyyy");

                //    lblCalle.Text = usuario.Direccion.Calle;
                //    lblNumero.Text = usuario.Direccion.Numero.ToString();
                //    lblPiso.Text = usuario.Direccion.Piso?.ToString() ?? "-";
                //    lblDepto.Text = usuario.Direccion.Depto ?? "-";
                //    lblLocalidad.Text = usuario.Direccion.Localidad;
                //    lblProvincia.Text = usuario.Direccion.Provincia;
                //    lblCodigoPostal.Text = usuario.Direccion.CodigoPostal.ToString();
                //}
                //else
                //{
                //    MostrarMensajeError("Usuario no encontrado.");
                //}
            }
            catch (Exception ex)
            {
                MostrarMensajeError("Ocurrió un error al cargar los datos del usuario. Detalle: " + ex.Message);
            }
        }

        private object ObtenerUsuarioPorId(int userId)
        {
            // Este método es un ejemplo. Debes implementarlo conectando tu capa de datos o negocio.
            return new
            {
                Nombre = "Juan",
                Apellido = "Pérez",
                Rol = "Administrador",
                Documento = 12345678,
                EmailPersonal = "juan.perez@example.com",
                Telefono = "123456789",
                FechaNacimiento = new DateTime(1985, 5, 20),
                Direccion = new
                {
                    Calle = "Av. Siempreviva",
                    Numero = 742,
                    Piso = 3,
                    Depto = "A",
                    Localidad = "Springfield",
                    Provincia = "Illinois",
                    CodigoPostal = 12345
                }
            };
        }

        private void MostrarMensajeError(string mensaje)
        {
            //lblError.Text = mensaje;
            //lblError.Visible = true;
        }
    }
}