using System;
using System.Web.UI;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class CreacionUsuario : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
             
                CargarEspecialidades();
                CargarRoles();
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string documento = txtDocumento.Text.Trim();
            string direccion = txtDireccion.Text.Trim();
            string matricula = txtMatricula.Text.Trim();
            string especialidad = ddlEspecialidad.SelectedValue;
            string rol = ddlRol.SelectedValue;

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido))
            {
             
                return;
            }

            Response.Write("<script>alert('Usuario creado con éxito.');</script>");

            LimpiarCampos();
        }

        private void CargarEspecialidades()
        {
           
        }

        private void CargarRoles()
        {
         
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtDocumento.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtMatricula.Text = string.Empty;
            ddlEspecialidad.SelectedIndex = 0; 
            ddlRol.SelectedIndex = 0; 
        }

      
        private void GuardarUsuario(string nombre, string apellido, string documento, string direccion, string matricula, string especialidad, string rol)
        {
        }
    }
}
