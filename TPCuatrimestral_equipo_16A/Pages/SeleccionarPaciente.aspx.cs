using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;
using Business;
using Business.Interfaces;
using Business.Managers;
using DataAccess;
using Domain;
using Domain.Entities;
using Unity;

namespace TPCuatrimestral_equipo_16A.Views
{
    public partial class SeleccionarPaciente : System.Web.UI.Page
    {

        private PacienteManager managerPaciente;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            

            if (!IsPostBack)
            {
                CargarPacientes();
                
                
                /*listaPacientes = managerPaciente.ObtenerTodos().Data;

                dgvPacientes.DataSource = listaPacientes;
                dgvPacientes.DataBind();*/
            }
        }


        private void CargarPacientes()
        {
            managerPaciente = (PacienteManager)Global.Container.Resolve(typeof(PacienteManager));
            List<Paciente> listaPacientes = new List<Paciente>();

            listaPacientes = managerPaciente.ObtenerTodos().Data;

            if (listaPacientes != null)
            {
                dgvPacientes.DataSource = listaPacientes;
                dgvPacientes.DataBind();
            }

        }
       
        protected void dgvPacientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            int idPaciente = Convert.ToInt32(dgvPacientes.SelectedDataKey.Value);

            string nombre = HttpUtility.HtmlDecode(dgvPacientes.SelectedRow.Cells[1].Text);
            string apellido = HttpUtility.HtmlDecode(dgvPacientes.SelectedRow.Cells[2].Text);
            string dni = dgvPacientes.SelectedRow.Cells[2].Text;

            txtBoxNombrePaciente.Text = nombre;
            txtBoxApellidoPaciente.Text = apellido;

            Session.Add("IdPaciente", idPaciente);
        }

        protected void txtBoxFiltrar_TextChanged(object sender, EventArgs e)
        {
            managerPaciente = (PacienteManager)Global.Container.Resolve(typeof(PacienteManager));


            string searchText = txtBoxFiltrar.Text.ToLower();

            // Filtramos la lista de especialidades según el texto buscado
            List<Paciente> filteredList = managerPaciente.ObtenerTodos()
                .Data // Usamos el Data de la respuesta
                .Where(paciente => paciente.Documento.ToString().ToLower().Contains(searchText))
                .ToList();

            dgvPacientes.DataSource = filteredList;
            dgvPacientes.DataBind();
        }

        protected void BtnSiguiente_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/SeleccionarSede.aspx", false);
        }

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Home.aspx", false);
        }
    }
}