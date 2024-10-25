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
    public partial class WebForm2 : System.Web.UI.Page
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
    }
}