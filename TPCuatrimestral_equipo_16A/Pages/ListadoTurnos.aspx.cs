using Business.Dtos;
using Business.Managers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_equipo_16A.Views
{
    public partial class ListadoTurnos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TurnoManager _manager = new TurnoManager();

                dgvTurnos.DataSource = _manager.ObtenerTodos();
                dgvTurnos.DataBind();
            }
        }

        protected void dgvTurnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            TurnoManager _manager = new TurnoManager();
            TurnoDTO turno = new TurnoDTO();

            GridViewRow selectedRow = dgvTurnos.SelectedRow;

            if (selectedRow != null)
            {
                string cellValue = selectedRow.Cells[0].Text;

                if (!string.IsNullOrEmpty(cellValue))
                {
                    turno = _manager.ObtenerPorId(int.Parse(cellValue));
                }

                try
                {
                    Session["TurnoDto"] = turno;
                    Session["ReprogramarTurno"] = true;

                    Session["IdMedico"] = turno.Medico.Id;
                    Session["DiaTurno"] = turno.Turno.Fecha;

                    Response.Redirect("~/Pages/SeleccionarFechaHora.aspx", false);
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }



        }

    }
}