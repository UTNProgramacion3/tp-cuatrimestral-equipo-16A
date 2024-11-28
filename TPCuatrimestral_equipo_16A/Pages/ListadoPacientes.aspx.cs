using Business.Interfaces;
using Business.Managers;
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
    public partial class ListadoPacientes : System.Web.UI.Page
    {
        private IPacienteManager _pacienteManager;
        private Usuario _usuario;

        private void InitDependencies()
        {
            IUnityContainer unityContainer;
            _pacienteManager = (IPacienteManager)Global.Container.Resolve(typeof(IPacienteManager));
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            InitDependencies();

            if (Session["UserLogueado"] != null)
            {
                _usuario = (Usuario)Session["UserLogueado"];

                if(_usuario.Rol.Id != (int)RolesEnum.Administrador)
                {
                    CargarPacientes();
                }
            }
        }

        private void CargarPacientes()
        {
            var res = _pacienteManager.ObtenerPacientesFiltrados("", "", "", "", "");

            if(res.Success)
            {
                gvPacientes.DataSource = res.Data;
                gvPacientes.DataBind();
            }

        }
    }
}