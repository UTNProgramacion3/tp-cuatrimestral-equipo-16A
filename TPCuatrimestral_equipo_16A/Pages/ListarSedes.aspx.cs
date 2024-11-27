using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Business.Dtos;
using Business.Interfaces;
using Business.Managers;
using DataAccess;
using Domain.Entities;
using Domain.Response;
using Unity;
using Utils;


namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class ListarSedes : System.Web.UI.Page
    {
        private DBManager dbManager;
        private SedeManager sedeManager;
        private IDireccionManager _direccionManager;

        private void InitDependencies()
        {
            IUnityContainer unityContainer;
            _direccionManager = (IDireccionManager)Global.Container.Resolve(typeof(IDireccionManager));

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            dbManager = new DBManager();
            sedeManager = new SedeManager();

            InitDependencies();

            //txtbSedeSeleccionada.Text = (string)Session["NombreSede"];

            if (!IsPostBack)
            {
                CargarSedes();
            }

        }

        private void CargarSedes()
        {
            Response<List<SedeDto>> listaSedes = sedeManager.ObtenerTodos();

            try
            {
                dgvSedes.DataSource = listaSedes.Data;
                dgvSedes.DataBind();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void dgvSedes_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnModificar.Enabled = true;
            
            GridViewRow selectedRow = dgvSedes.SelectedRow;

            if (selectedRow != null)
            {
                string cellValue = selectedRow.Cells[0].Text;
                string cellNombre = HttpUtility.HtmlDecode(selectedRow.Cells[1].Text);

                if (!string.IsNullOrEmpty(cellNombre))
                {
                    txtbSedeSeleccionada.Text = cellNombre;

                }

                if (!string.IsNullOrEmpty(cellValue) && int.TryParse(cellValue.Trim(), out int idSede))
                {
                    Session["IdSede"] = idSede;

                    CargarSede(int.Parse(cellValue));
                }
                else
                {
                    Response.Write("El valor seleccionado no es un ID válido.");
                }
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            int idSede = int.Parse(Session["IdSede"].ToString());

            CargarSede(idSede);
        }

        public void CargarSede(int idSede)
        {
            Response<Sede> sede = new Response<Sede>();
            Response<Direccion> direccion = new Response<Direccion>();

            sede = sedeManager.ObeterSedeById(idSede);
            direccion = _direccionManager.ObtenerPorId(sede.Data.DireccionId);

            Session["IdDireccion"] = direccion.Data.Id;



            txtNombreSede.Text = sede.Data.Nombre;
            txtCalleSede.Text = direccion.Data.Calle;
            txtNumero.Text = direccion.Data.Numero.ToString();
            txtPiso.Text = direccion.Data.Piso;
            txtDepto.Text = direccion.Data.Depto;
            txtLocalidad.Text = direccion.Data.Localidad;
            txtProvincia.Text = direccion.Data.Provincia;
            txtCodigoPostal.Text = direccion.Data.CodigoPostal;

        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            //txtbSedeSeleccionada.Text = (string)Session["NombreSede"];

            string searchText = txtBuscarSede.Text.Trim().ToLower();

            List<SedeDto> filteredList = sedeManager.ObtenerTodos()
                .Data
                .Where(SedeDto => SedeDto.Sede.Nombre.Trim().ToLower().Contains(searchText))
                .ToList();

            dgvSedes.DataSource = filteredList;
            dgvSedes.DataBind();
        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {

            Session["IdDireccion"] = null;
            Session["IdSede"] = null;
            Session["NombreSede"] = null;

            btnModificar.Enabled = false;

            //int idSede = (int)Session["IdSede"];

            if (txtBuscarSede.Text != "")
            {
                txtBuscarSede.Text = "";
                CargarSedes();
            }


        }

        protected void btnModificarSede_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtCrearNombreSede.Text)
                && !string.IsNullOrEmpty(txtCrearCalleSede.Text)
                && !string.IsNullOrEmpty(txtCrearNumeroSede.Text)
                && !string.IsNullOrEmpty(txtCrearLocalidadSede.Text)
                && !string.IsNullOrEmpty(txtCrearProvinciaSede.Text))
            {

                Direccion _direccion = new Direccion();
                Sede _sede = new Sede();

                try
                {
                    _direccion.Numero = int.Parse(txtNumero.Text);
                }
                catch (Exception)
                {
                    campoInvalidoNumerSede.Visible = true;
                    return;
                }

                _direccion.Id = int.Parse(Session["IdDireccion"].ToString());
                _direccion.Calle = txtCalleSede.Text.Trim();
                _direccion.Piso = string.IsNullOrWhiteSpace(txtPiso.Text) ? "" : txtPiso.Text.Trim();
                _direccion.Depto = string.IsNullOrWhiteSpace(txtDepto.Text) ? "" : txtDepto.Text.Trim();
                _direccion.Localidad = txtLocalidad.Text.Trim();
                _direccion.Provincia = txtProvincia.Text.Trim();
                _direccion.CodigoPostal = string.IsNullOrWhiteSpace(txtCodigoPostal.Text) ? "" : txtCodigoPostal.Text.Trim();


                try
                {

                    var resDreccion = _direccionManager.Update(_direccion);

                    if (resDreccion.Success)
                    {
                        _sede.Id = int.Parse(Session["IdSede"].ToString());
                        _sede.Nombre = txtNombreSede.Text;
                        _sede.DireccionId = _direccion.Id;

                        var res = sedeManager.Update(_sede);

                        if (res != null)
                        {
                            modificarSedeSucces.Visible = true;
                            campoInvalidoNumerSede.Visible = false;
                        }
                        else
                        {
                            modificarDedeFailure.Visible = true;

                        }

                    }

                }
                catch (Exception ex)
                {

                    modificarDedeFailure.Visible = true;

                    throw ex;
                }
            }
            else
            {
                modificarDedeFailure.Visible = true;
            }
            
            ReloadSedes();

        }

        protected void btnGuardarSede_Click(object sender, EventArgs e)
        {
            
            if(!string.IsNullOrEmpty(txtCrearNombreSede.Text)
                && !string.IsNullOrEmpty(txtCrearCalleSede.Text)
                && !string.IsNullOrEmpty(txtCrearNumeroSede.Text)
                && !string.IsNullOrEmpty(txtCrearLocalidadSede.Text)
                && !string.IsNullOrEmpty(txtCrearProvinciaSede.Text))
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

                try
                {
                    _direccion.Numero = int.Parse(txtCrearNumeroSede.Text);
                }
                catch (Exception)
                {
                    campoInvalidoNumerSede.Visible = true;
                    return;
                }

                _direccion.Calle = txtCrearCalleSede.Text.Trim();
                _direccion.Piso = string.IsNullOrWhiteSpace(txtCrearPisoSede.Text) ? "" : txtCrearPisoSede.Text.Trim();
                _direccion.Depto = string.IsNullOrWhiteSpace(txtCrearDeptoSede.Text) ? "": txtCrearDeptoSede.Text.Trim();
                _direccion.Localidad = txtCrearLocalidadSede.Text.Trim();
                _direccion.Provincia = txtCrearProvinciaSede.Text.Trim();
                _direccion.CodigoPostal = string.IsNullOrWhiteSpace(txtCrearCodigoPostalSede.Text) ? "" : txtCrearCodigoPostalSede.Text.Trim();



                try
                {
                    var resDreccion = _direccionManager.Crear(_direccion);

                    _sede.Data.Sede.Nombre = txtCrearNombreSede.Text;
                    _sede.Data.Direccion.Id = resDreccion.Data.Id;

                    var sedeDto = _sede.Data;

                    sedeManager.Crear(sedeDto);

                    crearSedeSuccess.Visible = true;



                }
                catch (Exception ex)
                {
                    crearSedeFailure.Visible = false;



                    throw ex;
                }
            }
            else
            {
                crearSedeFailure.Visible = true;
            }

            ReloadSedes();

        }

        protected void ReloadSedes()
        {
            string script = $@"
               setTimeout(function() {{
            window.location.href = '/Pages/ListarSedes.aspx';
               }},5000);";

            ClientScript.RegisterStartupScript(this.GetType(), "DelayedRedirect", script, true);
        }

        protected void btnAtras_Click1(object sender, EventArgs e)
        {

            Session["IdDireccion"] = null;
            Session["IdSede"] = null;
            Session["NombreSede"] = null;

            Response.Redirect("~/Pages/Home.aspx", false);

        }
    }
}