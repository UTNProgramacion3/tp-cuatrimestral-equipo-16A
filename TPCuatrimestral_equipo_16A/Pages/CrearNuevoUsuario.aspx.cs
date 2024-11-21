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

        private void InitDependencies()
        {
            IUnityContainer unityContainer;
            _pacienteManager = (IPacienteManager)Global.Container.Resolve(typeof(IPacienteManager));
            _empleadoManager = (IEmpleadoManager)Global.Container.Resolve(typeof(IEmpleadoManager));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitDependencies();
            if (!IsPostBack)
            {
                ddlRol.Items.Clear(); // Limpia cualquier opción existente para evitar duplicados

                ddlRol.Items.Add(new ListItem("Administrador", "1"));
                ddlRol.Items.Add(new ListItem("Empleado", "2"));
                ddlRol.Items.Add(new ListItem("Paciente", "3"));
                CargarEspecialidades();
                CargarRoles();
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string documento = txtDocumento.Text.Trim();
            string matricula = txtMatricula.Text.Trim();
            string especialidad = ddlEspecialidad.SelectedValue;
            int rol = int.Parse(ddlRol.SelectedValue);
            string legajo = txtLegajo.Text.Trim();
            int posicion = int.Parse(posicionEmpleado.SelectedValue);

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(documento))
            {
                Response.Write("<script>alert('Por favor, complete todos los campos obligatorios.');</script>");
                return;
            }

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
                case (int)RolesEnum.Empleado:
                    NuevoEmpleadoDto nuevoEmpleado = new NuevoEmpleadoDto
                    {
                        Apellido = apellido,
                        Nombre = nombre,
                        Documento = int.Parse(documento),
                        Direccion = direccion,
                        Legajo = int.Parse(legajo),
                        EmailPersonal = txtEmailPersonal.Text.Trim(),
                        Telefono = txtTelefono.Text.Trim(),
                        FechaNacimiento = txtFechaNacimiento.SelectedDate,
                        Posicion = posicion,
                        Matricula = int.Parse(txtMatricula.Text),
                        EspecialidadId = int.Parse(ddlEspecialidad.SelectedValue),
                        RolId = (int)RolesEnum.Empleado
                    };
                    _empleadoManager.CrearNuevo(nuevoEmpleado);
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
                        FechaNacimiento = txtFechaNacimiento.SelectedDate,
                        RolId = (int)RolesEnum.Paciente

                    };
                    _pacienteManager.Crear(nuevoPaciente);
                    break;
            }

            //// Lógica según el rol seleccionado
            //if (rol == (int)RolesEnum.Empleado)
            //{
            //    // Validación para médico (Matricula y Especialidad deben ser completados)
            //    if (string.IsNullOrEmpty(matricula) || string.IsNullOrEmpty(especialidad))
            //    {
            //        Response.Write("<script>alert('Por favor, complete los campos de Matrícula y Especialidad para el Médico.');</script>");
            //        return;
            //    }

            //    // Aquí puedes agregar la lógica para crear un Médico y guardar en base de datos.
            //    GuardarUsuario(nombre, apellido, documento, direccion, matricula, especialidad, rol);
            //}
            //else if (rol == (int)RolesEnum.Empleado)
            //{
            //    // Validación para Empleado (Legajo debe ser completado)
            //    string legajo = "123345"
            //    if (string.IsNullOrEmpty(legajo))
            //    {
            //        Response.Write("<script>alert('Por favor, complete el campo de Legajo para el Empleado.');</script>");
            //        return;
            //    }

            //    // Aquí puedes agregar la lógica para crear un Empleado y guardar en base de datos.
            //    GuardarUsuario(nombre, apellido, documento, direccion, legajo, string.Empty, rol);
            //}
            //else if (rol == (int)RolesEnum.Paciente)
            //{
            //    // Lógica para Paciente (sin campos adicionales)
            //    GuardarUsuario(nombre, apellido, documento, direccion, string.Empty, string.Empty, rol);
            //}

            Response.Write("<script>alert('Usuario creado con éxito.');</script>");
            LimpiarCampos();
        }

        private void CargarEspecialidades()
        {
            // Cargar las especialidades (esto puede venir de una base de datos)
            ddlEspecialidad.Items.Clear();
            ddlEspecialidad.Items.Add(new ListItem("Especialidad 1", "1"));
            ddlEspecialidad.Items.Add(new ListItem("Especialidad 2", "2"));
            ddlEspecialidad.Items.Add(new ListItem("Especialidad 3", "3"));
        }

        private void CargarRoles()
        {
            ddlRol.Items.Clear();
            ddlRol.Items.Add(new ListItem("Administrador", "1"));
            ddlRol.Items.Add(new ListItem("Empleado", "4"));
            ddlRol.Items.Add(new ListItem("Paciente", "5"));
        }


        private void LimpiarCampos()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtDocumento.Text = string.Empty;
            txtMatricula.Text = string.Empty;
            ddlEspecialidad.SelectedIndex = 0;
            ddlRol.SelectedIndex = 0;
            txtLegajo.Text = string.Empty;
        }

        protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rolSeleccionado = int.Parse(ddlRol.SelectedValue);
        }


    }
}