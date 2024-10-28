using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Business.Managers;
using Domain;

namespace TPCuatrimestral_equipo_16A.Views
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //PacienteManager _manager = new PacienteManager();

                //dgvPacientes.DataSource = _manager.ObtenerTodos();

                dgvPacientes.DataSource = ObtenerDatos();
                dgvPacientes.DataBind();
            }
        }

        protected DataTable ObtenerDatos()
        {
            
            DataTable dt = new DataTable();
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Apellido");
            dt.Columns.Add("DNI");

            
            dt.Rows.Add("Juan", "Perez", "654321");
            dt.Rows.Add("Pablo", "Gonzalez", "654987");
            dt.Rows.Add("Alberto", "Pascal", "123456");

            return dt;
        }

        protected void dgvPacientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            int index = dgvPacientes.SelectedIndex;

            
            string nombre = dgvPacientes.SelectedRow.Cells[0].Text;
            string apellido = dgvPacientes.SelectedRow.Cells[1].Text;
            string dni = dgvPacientes.SelectedRow.Cells[2].Text;

            inputNombrePaciente.Value = nombre;
            inputApellidoPaciente.Value = apellido;
        }

        protected void BtnSiguiente_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/SeleccionarTurno.aspx", false);
        }
    }
}