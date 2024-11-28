using Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class MenuUsuario : System.Web.UI.Page
    {

        private IPacienteManager _pacienteManager;

        private void InitDependencies()
        {
            IUnityContainer unityContainer;
            _pacienteManager = (IPacienteManager)Global.Container.Resolve(typeof(IPacienteManager));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitDependencies();

            if (!IsPostBack)
            {
                var tarjetas = new List<dynamic>
        {
            new { Titulo = "Crear Usuario", Descripcion = "Crea un nuevo Usuario", Url = "/Pages/CrearNuevoUsuario.aspx" },
            new { Titulo = "Listar Usuarios", Descripcion = "Permite visualizar usuarios existentes", Url = "/Pages/ListadoUsuarios.aspx" },
        };
                rptTarjetas.DataSource = tarjetas;
                rptTarjetas.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //_emailManager.EnviarMailValidacionNuevaCuenta("escuderopablo.m@gmail.com", 1);
            var res = _pacienteManager.ObtenerPorId(1);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pages/CrearNuevoUsuario.aspx");
        }
    }
}