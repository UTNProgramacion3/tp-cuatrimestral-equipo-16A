using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Dtos;
using DataAccess;
using Business.Managers;
using Domain.Enums;
using Domain.Entities;
using Domain.Response;
using Business.Interfaces;
using Unity;

namespace TPCuatrimestral_equipo_16A.Pages
{
    public partial class ConfirmarTurno : System.Web.UI.Page
    {   

        DBManager dbManager;
        TurnoManager turnoManager;

        private IDireccionManager _direccionManager;
        private IPacienteManager _pacienteManager;
        private ISedeManager _sedeManager;
        private IMedicoManager _medicoManager;
        private IEspecialidadManager _especialidadManager;

        private void InitDependencies()
        {
            IUnityContainer unityContainer;
            _direccionManager = (IDireccionManager)Global.Container.Resolve(typeof(IDireccionManager));
            _pacienteManager = (IPacienteManager)Global.Container.Resolve(typeof(IPacienteManager));
            _sedeManager = (ISedeManager)Global.Container.Resolve(typeof(ISedeManager));
            _medicoManager = (IMedicoManager)Global.Container.Resolve(typeof(IMedicoManager));
            _especialidadManager = (IEspecialidadManager)Global.Container.Resolve(typeof(IEspecialidadManager));

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            InitDependencies();
            
            dbManager = new DBManager();
            turnoManager = new TurnoManager();

            if (!IsPostBack)
            {
                ObtenerTurno();
            }
        }

        protected void ObtenerTurno()
        {
            TurnoDTO turnoReprogramar = new TurnoDTO();
            turnoReprogramar = (TurnoDTO)Session["TurnoDto"];

            if (Session["ReprogramarTurno"] != null)
            {
                btnAtras.Visible = false;

                try
                {
                    Response<Paciente> pacienteReprogramar = _pacienteManager.ObtenerPorIdTurno(turnoReprogramar.Paciente.Id);
                    Response<Medico> medicoReprogramar = _medicoManager.ObtenerMedicoById(turnoReprogramar.Medico.Id);
                    Response<Especialidad> especialidadReprogramar = _especialidadManager.ObtenerPorId(medicoReprogramar.Data.EspecialidadId);
                    Response<Sede> sedeReprogramar = _sedeManager.ObeterSedeById(turnoReprogramar.Sede.Id);
                    Response<Direccion> direccionReprogramar = _direccionManager.ObtenerPorId(sedeReprogramar.Data.DireccionId);

                    string direccionReprogramarString = direccionReprogramar.Data.Calle.ToString() + " " + direccionReprogramar.Data.Numero.ToString() + " " + direccionReprogramar.Data.Provincia.ToString();

                    lblNombrePaciente.Text = pacienteReprogramar.Data.Nombre.ToString();
                    lblApellidoPaciente.Text = pacienteReprogramar.Data.Apellido.ToString();
                    lblNombreMedico.Text = medicoReprogramar.Data.Nombre.ToString();
                    lblApellidoMedico.Text = medicoReprogramar.Data.Apellido.ToString();
                    lblEspecialidad.Text = especialidadReprogramar.Data.Nombre.ToString();
                    lblNombreSede.Text = sedeReprogramar.Data.Nombre.ToString();
                    lblDireccionSede.Text = direccionReprogramarString;
                    lblFecha.Text = (string)Session["FechaTurno"].ToString();
                    lblHora.Text = (string)Session["HoraTurno"].ToString();
                    txtbObservaciones.InnerText = turnoReprogramar.Turno.Observaciones.ToString();

                    Session["IdPaciente"] = pacienteReprogramar.Data.Id;
                    Session["IdMedico"] = medicoReprogramar.Data.Id;
                    Session["IdSede"] = sedeReprogramar.Data.Id;
                    Session["IdEstadoTurno"] = (int)EstadosEnum.Confirmado;
                    Session["IdTurnoAreprogramar"] = turnoReprogramar.Turno.Id;

                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
            else if (Session["CancelarTurno"] != null)
            {
                btnAtras.Visible = false;
                btnConfirmar.Text = "Confirma Cancelacion de Turno";
                cardHeader.InnerHtml = "<h2>Cancelar Turno</h2>";

                try
                {
                    Response<Paciente> pacienteCancelar = _pacienteManager.ObtenerPorId(turnoReprogramar.Paciente.Id);
                    Response<Medico> medicoCancelar = _medicoManager.ObtenerMedicoById(turnoReprogramar.Medico.Id);
                    Response<Especialidad> especialidadCancelar = _especialidadManager.ObtenerPorId(medicoCancelar.Data.EspecialidadId);
                    Response<Sede> sedeCancelar = _sedeManager.ObeterSedeById(turnoReprogramar.Sede.Id);
                    Response<Direccion> direccionCancelar = _direccionManager.ObtenerPorId(sedeCancelar.Data.DireccionId);

                    string direccionReprogramarString = direccionCancelar.Data.Calle.ToString() + " " + direccionCancelar.Data.Numero.ToString() + " " + direccionCancelar.Data.Provincia.ToString();

                    lblNombrePaciente.Text = pacienteCancelar.Data.Nombre.ToString();
                    lblApellidoPaciente.Text = pacienteCancelar.Data.Apellido.ToString();
                    lblNombreMedico.Text = medicoCancelar.Data.Nombre.ToString();
                    lblApellidoMedico.Text = medicoCancelar.Data.Apellido.ToString();
                    lblEspecialidad.Text = especialidadCancelar.Data.Nombre.ToString();
                    lblNombreSede.Text = sedeCancelar.Data.Nombre.ToString();
                    lblDireccionSede.Text = direccionReprogramarString;
                    lblFecha.Text = (string)Session["DiaTurno"].ToString();
                    lblHora.Text = (string)Session["HoraTurno"].ToString();
                    txtbObservaciones.InnerText = turnoReprogramar.Turno.Observaciones.ToString();

                    Session["IdPaciente"] = pacienteCancelar.Data.Id;
                    Session["IdMedico"] = medicoCancelar.Data.Id;
                    Session["IdSede"] = sedeCancelar.Data.Id;
                    Session["IdEstadoTurno"] = (int)EstadosEnum.Cancelado;
                    Session["IdTurnoAreprogramar"] = turnoReprogramar.Turno.Id;
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
            else
            {
                lblNombrePaciente.Text = (string)Session["NombrePaciente"];
                lblApellidoPaciente.Text = (string)Session["ApellidoPaciente"];
                lblNombreMedico.Text = (string)Session["NombreMedico"];
                lblApellidoMedico.Text = (string)Session["ApellidoMedico"];
                lblEspecialidad.Text = (string)Session["Especialidad"];
                lblNombreSede.Text = (string)Session["NombreSede"];
                lblDireccionSede.Text = (string)Session["DireccionSede"];
                lblFecha.Text = (string)Session["FechaTurno"];
                lblHora.Text = (string)Session["HoraTurno"];
            }
        }

        protected void ReasignarOCancelar()
        {
            TurnoDTO turno = new TurnoDTO();

            turno.Paciente.Id = (int)Session["IdPaciente"];
            turno.Medico.Id = (int)Session["IdMedico"];
            turno.Sede.Id = (int)Session["IdSede"];
            turno.EstadoTurno.Id = (int)Session["IdEstadoTurno"];
            turno.Turno.Observaciones = (string)Session["Observaciones"];
            
            if (Session["CancelarTurno"] != null)
            {
                //Aca Cancelamos
                var dia = Session["DiaTurno"];
                turno.Turno.Fecha = (DateTime)Session["DiaTurno"];
                turno.Turno.Hora = (TimeSpan)Session["HoraTurno"];

                int idReprogramar = (int)EstadosEnum.Cancelado;
                int idTurnoAcancelar = (int)Session["IdTurnoAreprogramar"];
                turnoManager.CancelarOReprogramarTurno(idReprogramar, idTurnoAcancelar);
            }
            else
            {
                //Aca reprogramamos y se crea un turno nuevo

                turno.Turno.Fecha = DateTime.Parse((string)Session["FechaTurno"]);
                turno.Turno.Hora = TimeSpan.Parse((string)Session["HoraTurno"]);

                turnoManager.Crear(turno);
                int idReprogramar = (int)EstadosEnum.Reprogramado;
                int idTurnoAreprogramar = (int)Session["IdTurnoAreprogramar"];
                turnoManager.CancelarOReprogramarTurno(idReprogramar, idTurnoAreprogramar);
            }

            
            

        }

        protected void CrearTurno()
        {
            TurnoDTO turno = new TurnoDTO();

            turno.Paciente.Id = (int)Session["IdPaciente"];
            turno.Medico.Id = (int)Session["IdMedico"];
            turno.Sede.Id = (int)Session["IdSede"];
            turno.EstadoTurno.Id = (int)Session["IdEstadoTurno"];
            turno.Turno.Fecha = DateTime.Parse((string)Session["FechaTurno"]);
            turno.Turno.Hora = TimeSpan.Parse((string)Session["HoraTurno"]);
            turno.Turno.Observaciones = (string)Session["Observaciones"];


            turnoManager.Crear(turno);
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            btnConfirmar.Enabled = false;
            btnCancelar.Enabled = false;
            turnoSuccess.Visible = true;
            
            TurnoConfirmado();

            if (Session["CancelarTurno"] != null || Session["ReprogramarTurno"] != null)
            {
                ReasignarOCancelar();
            }
            else
            {
                CrearTurno();
            }


            RedirectHome();

        }

        protected void TurnoConfirmado()
        {
            if(txtbObservaciones != null)
            {
                Session["Observaciones"] = txtbObservaciones.InnerText;
            }
            else
            {
                Session["Observaciones"] = "";
            }
            

            Session["IdEstadoTurno"] = (int)EstadosEnum.Confirmado;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            btnConfirmar.Enabled = false;
            btnCancelar.Enabled = false;
            turnoCancelado.Visible = true;

            RedirectHome();
        }

        protected void RedirectHome()
        {
            string script = $@"
               setTimeout(function() {{
            window.location.href = '/Pages/Home.aspx';
               }},5000);";

            ClientScript.RegisterStartupScript(this.GetType(), "DelayedRedirect", script, true);

            Session["NombrePaciente"] = null;
            Session["ApellidoPaciente"] = null;
            Session["NombreMedico"] = null;
            Session["ApellidoMedico"] = null;
            Session["Especialidad"] = null;
            Session["NombreSede"] = null;
            Session["DireccionSede"] = null;
            Session["FechaTurno"] = null;
            Session["HoraTurno"] = null;
            Session["IdPaciente"] = null;
            Session["IdMedico"] = null;
            Session["IdSede"] = null;
            Session["IdEstadoTurno"] = null;
            Session["Observaciones"] = null;
            Session["IdPaciente"] = null;
            Session["IdMedico"] = null;
            Session["IdSede"] = null;
            Session["IdEstadoTurno"] = null;
            Session["IdTurnoAreprogramar"] = null;
            Session["CancelarTurno"] = null;
            Session["DiaTurno"] = null;
        }


        protected void btnAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/SeleccionarFechaHora.aspx", false);
        }
    }
}