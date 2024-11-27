using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class JornadaLaboralEmpleado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                int id = int.Parse(Request.QueryString["id"]);
                // Cargar la jornada laboral para este ID si es necesario
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Obtener las horas de trabajo desde los campos
            //var lunesInicio = lunesInicio.Value;
            //var lunesFin = lunesFin.Value;
            //var martesInicio = martesInicio.Value;
            //var martesFin = martesFin.Value;
            //var miercolesInicio = miercolesInicio.Value;
            //var miercolesFin = miercolesFin.Value;
            //var juevesInicio = juevesInicio.Value;
            //var juevesFin = juevesFin.Value;
            //var viernesInicio = viernesInicio.Value;
            //var viernesFin = viernesFin.Value;
            //var sabadoInicio = sabadoInicio.Value;
            //var sabadoFin = sabadoFin.Value;
            //var domingoInicio = domingoInicio.Value;
            //var domingoFin = domingoFin.Value;

            // Validar y procesar la jornada laboral (se puede almacenar en base de datos)

            // Redirigir después de guardar los datos
            Response.Redirect("Confirmacion.aspx");
        }
    }
}