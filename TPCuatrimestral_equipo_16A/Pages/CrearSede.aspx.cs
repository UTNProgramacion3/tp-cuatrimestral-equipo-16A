using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Managers;
using Domain.Response;
using Domain.Entities;
using Business.Dtos;
using static System.Net.Mime.MediaTypeNames;
using Business.Interfaces;
using Unity;


namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class AgregarSede : System.Web.UI.Page
    {

        private ISedeManager _sedeManager;
        private IDireccionManager _direccionManager;

        private void InitDependencies()
        {
            IUnityContainer unityContainer;
            _sedeManager = (ISedeManager)Global.Container.Resolve(typeof(ISedeManager));
            _direccionManager = (IDireccionManager)Global.Container.Resolve(typeof(IDireccionManager));

        }

       


        protected void Page_Load(object sender, EventArgs e)
        {
            InitDependencies();

        }

        public void CrearSede()
        {
            Response<SedeDto> _sede = new Response<SedeDto>
            {
                Data = new SedeDto
                {
                    Sede = new Sede(),
                    Direccion = new Direccion()
                }
            };

            Direccion _direccion = new Direccion();

            _direccion.Calle = txtCalleSede.Text.Trim();
            _direccion.Numero = int.Parse(txtNumero.Text);
            _direccion.Piso = txtPiso.Text.Trim();
            _direccion.Depto = txtDepto.Text.Trim();
            _direccion.Localidad = txtLocalidad.Text.Trim();
            _direccion.Provincia = txtProvincia.Text.Trim();
            _direccion.CodigoPostal = txtCodigoPostal.Text.Trim();



            try
            {
                var resDreccion = _direccionManager.Crear(_direccion);

                _sede.Data.Sede.Nombre = txtNombreSede.Text;
                _sede.Data.Direccion.Id = resDreccion.Data.Id;

                var sedeDto = _sede.Data;

                _sedeManager.Crear(sedeDto);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnGuardarSede_Click(object sender, EventArgs e)
        {
            CrearSede();
        }
    }
}