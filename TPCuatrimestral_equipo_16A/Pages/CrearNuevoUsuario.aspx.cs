using Business.Dtos;
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
    public partial class CrearNuevoUsuario : System.Web.UI.Page
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


            if (!IsPostBack)
            {
                ddlRol.Items.Clear();

                ddlRol.Items.Add(new ListItem("Administrador", "1"));
                ddlRol.Items.Add(new ListItem("Empleado", "2"));
                ddlRol.Items.Add(new ListItem("Paciente", "3"));
                CargarEspecialidades();
                CargarRoles();
            }

            if (_isEditModeEnabled)
            {
                var user = _usuarioManager.ObtenerUsuarioById(int.Parse(Request.QueryString["id"]));
                btnCrear.Text = "Editar";

                switch (user.RolId)
                {
                    //case (int)RolesEnum.Administrador:
                    //    var admin = _usuarioManager.ObtenerAdminByUserId(user.Id);
                    //    cargarDatosPersonaFormularioEditar((Persona)admin);
                    //    cargarDireccionFormularioEditar(admin.Direccion);
                    //    break;
                    case (int)RolesEnum.Empleado:
                    case (int)RolesEnum.Medico:
                        var medico = _medicoManager.ObtenerMedicoByUserId(user.Id);
                        cargarMedicoAEditar(medico);
                        break;
                    case (int)RolesEnum.Paciente:
                        var paciente = _pacienteManager.ObtenerPacienteByUserId(user.Id);
                        cargarPacienteAEditar(paciente);
                        break;
                }
            }


        }
        //private void cargarAdminAEditar(NuevoAdminDto admin)
        //{
        //    ddlRol.SelectedIndex = admin.RolId;
        //    cargarDatosPersonaFormularioEditar((Persona)admin);
        //    cargarDireccionFormularioEditar(admin.Direccion);
        //}

        private void cargarMedicoAEditar(Medico medico)
        {
            ddlRol.SelectedIndex = medico.RolId;
            cargarDatosPersonaFormularioEditar((Persona)medico);
            cargarDireccionFormularioEditar(medico.Direccion);
            cargarDatosEmpleadoFormularioEditar((Empleado)medico);

            txtMatricula.Text = medico.Matricula.ToString();
            ddlEspecialidad.SelectedValue = medico.EspecialidadId.ToString();
        }


        private void cargarPacienteAEditar(Paciente paciente)
        {
            txtNombre.Text = paciente.Nombre;
            txtApellido.Text = paciente.Apellido;
            txtEmailPersonal.Text = paciente.EmailPersonal;
            txtFechaNacimiento.Text = paciente.FechaNacimiento.ToString("dd-MM-yyyy");
        }

        private void cargarDatosPersonaFormularioEditar(Persona persona)
        {
            txtNombre.Text = persona.Nombre;
            txtApellido.Text = persona.Apellido;
            txtEmailPersonal.Text = persona.EmailPersonal;
            txtFechaNacimiento.Text = persona.FechaNacimiento.ToString("yyyy-MM-dd");
            txtTelefono.Text = persona.Telefono;
            txtDocumento.Text = persona.Documento.ToString();
        }

        private void cargarDireccionFormularioEditar(Direccion direccion)
        {
            txtCalle.Text = direccion.Calle;
            txtNumero.Text = direccion.Numero.ToString();
            txtLocalidad.Text = direccion.Localidad;
            txtDepto.Text = direccion.Depto;
            txtPiso.Text = direccion.Piso;
            txtProvincia.Text = direccion.Provincia;
            txtCodigoPostal.Text = direccion.CodigoPostal;
        }

        private void cargarDatosEmpleadoFormularioEditar(Empleado empleado)
        {
            txtLegajo.Text = empleado.Legajo.ToString();
            ddlRol.SelectedValue = empleado.RolId.ToString();
            //posicionEmpleado.SelectedValue = empleado.Posicion.ToString();
        }

        private void cargarDatosMedicoFormularioEditar(Medico medico)
        {
            txtMatricula.Text = medico.Matricula.ToString();
            ddlEspecialidad.SelectedValue = medico.EspecialidadId.ToString();
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string documento = txtDocumento.Text.Trim();
            string matricula = txtMatricula.Text.Trim();
            string especialidad = ddlEspecialidad.SelectedValue;
            int rol = int.Parse(ddlRol.SelectedValue);
            //string legajo = txtLegajo.Text.Trim();
            int posicion = int.Parse(ddlRol.SelectedValue);

            Direccion direccion = new Direccion
            {
                Calle = txtCalle.Text.Trim(),
                Numero = int.TryParse(txtNumero.Text.Trim(), out int numero) ? numero : 0,
                Piso = txtPiso.Text.Trim(),
                Depto = txtDepto.Text.Trim(),
                Localidad = txtLocalidad.Text.Trim(),
                Provincia = txtProvincia.Text.Trim(),
                CodigoPostal = txtCodigoPostal.Text.Trim()
            };

            switch (rol)
            {
                case (int)RolesEnum.Administrador:

                    NuevoAdminDto nuevoAdmin = new NuevoAdminDto()
                    {
                        Apellido = apellido,
                        Nombre = nombre,
                        Documento = int.Parse(documento),
                        EmailPersonal = txtEmailPersonal.Text.Trim(),
                        Telefono = txtTelefono.Text.Trim(),
                        FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text),
                        Passwordhash = documento,
                        RolId = (int)RolesEnum.Administrador

                    };

                    _usuarioManager.CrearNuevoAdmin(nuevoAdmin);
                    break;
                case (int)RolesEnum.Empleado:
                case (int)RolesEnum.Medico:
                    NuevoEmpleadoDto nuevoEmpleado = new NuevoEmpleadoDto
                    {
                        Apellido = apellido,
                        Nombre = nombre,
                        Documento = int.Parse(documento),
                        Direccion = direccion,
                        //Legajo = int.Parse(legajo),
                        EmailPersonal = txtEmailPersonal.Text.Trim(),
                        Telefono = txtTelefono.Text.Trim(),
                        FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text),
                        Posicion = posicion,
                        Matricula = int.Parse(txtMatricula.Text),
                        EspecialidadId = int.Parse(ddlEspecialidad.SelectedValue),
                        RolId = rol
                    };
                    if (_isEditModeEnabled)
                    {

                    }
                    else
                    {
                        var res = _empleadoManager.CrearNuevo(nuevoEmpleado);
                        if (res.Success)
                        {
                            Response.Redirect("AsignarJornadaLaboral.aspx?id=" + res.Data.Id);
                        }
                    }
                    break;

                case (int)RolesEnum.Paciente:

                    Paciente nuevoPaciente = new Paciente
                    {
                        Apellido = apellido,
                        Nombre = nombre,
                        Documento = int.Parse(documento),
                        Direccion = direccion,
                        EmailPersonal = txtEmailPersonal.Text.Trim(),
                        Telefono = txtTelefono.Text.Trim(),
                        FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text),
                        RolId = (int)RolesEnum.Paciente

                    };
                    if (_isEditModeEnabled)
                    {

                    }
                    else
                    {
                        _pacienteManager.Crear(nuevoPaciente);

                    }
                    break;
            }

            string titulo = $"Éxito creando {rol}";
            string texto = $"El registro de {nombre} {apellido} se creó correctamente.";

            string script = $"mostrarMensaje('{titulo}', '{texto}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "MostrarMensaje", script, true);
            LimpiarCampos();
        }

        private void CargarEspecialidades()
        {
            var especialides = _medicoManager.ObtenerTodasEspecialidades();
            ddlEspecialidad.Items.Clear();
            foreach (var especialidad in especialides)
            {
                ddlEspecialidad.Items.Add(new ListItem(especialidad.Nombre, especialidad.Id.ToString()));
            }
        }

        private void CargarRoles()
        {
            var roles = _usuarioManager.ObtenerAllRoles();
            ddlRol.Items.Clear();
            foreach(var rol in roles)
            {
                ddlRol.Items.Add(new ListItem(rol.Nombre, rol.Id.ToString()));
            }
        }


        private void LimpiarCampos()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtDocumento.Text = string.Empty;
            txtMatricula.Text = string.Empty;
            ddlEspecialidad.SelectedIndex = 0;
            ddlRol.SelectedIndex = 0;
            //txtLegajo.Text = string.Empty;
        }

        protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rolSeleccionado = int.Parse(ddlRol.SelectedValue);
        }


    }
}