using Business.Interfaces;
using DataAccess;
using DataAccess.Extensions;
using Domain.Entities;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Utils;
using Utils.Interfaces;

namespace Business.Managers
{
    public class PacienteManager : IPacienteManager
    {
        #region Private properties
        private readonly DBManager _DBManager;
        private readonly Response<Paciente> _response;
        private readonly IDireccionManager _direccionManager;
        private readonly IPersonaManager _personaManager;
        private readonly IUsuarioManager _usuarioManager;
        private readonly IEmailManager _emailManager;
        private readonly IMapper<Paciente> _mapper;
        private readonly string _VALIDAR_CON_EMAIL;

        #endregion

        #region Builder
        public PacienteManager(
            DBManager manager,
            Response<Paciente> response,
            IDireccionManager direccionManager,
            IPersonaManager personaManager,
            IUsuarioManager usuarioManager,
            IEmailManager emailManager
            )
            
        {
            _DBManager = manager;
            _response = response;
            _direccionManager = direccionManager;
            _personaManager = personaManager;
            _usuarioManager = usuarioManager;
            _emailManager = emailManager;
            _mapper = new Mapper<Paciente>();
            _VALIDAR_CON_EMAIL = Environment.GetEnvironmentVariable("VALIDAR_CON_EMAIL");

        }
        #endregion

        #region Public methods
        public Response<Paciente> Crear(Paciente entity)
        {
            try
            {
                bool validarConEmail = _VALIDAR_CON_EMAIL == "true";
                var user = _usuarioManager.GenerarUsuario((Persona)entity, entity.RolId);
                user.Activo = !validarConEmail;
                var usuarioCreado = _usuarioManager.Crear(user);
                var nombreUsuario = $"{entity.Apellido}, {entity.Nombre}";

                if (validarConEmail)
                {
                    _emailManager.EnviarMailValidacionNuevaCuenta(entity.EmailPersonal, usuarioCreado.Data.Id, nombreUsuario);
                }

                if (!usuarioCreado.Success)
                {
                    throw new Exception("Error al crear el usuario");
                }
                var direccionCreada = _direccionManager.Crear(entity.Direccion);

                if (!direccionCreada.Success)
                {
                    throw new Exception("Error al crear la dirección");
                }

                var persona = new Persona()
                {
                    Apellido = entity.Apellido,
                    Nombre = entity.Nombre,
                    Documento = entity.Documento,
                    EmailPersonal = entity.EmailPersonal,
                    FechaNacimiento = entity.FechaNacimiento,
                    Telefono = entity.Telefono,
                    DireccionId = direccionCreada.Data.Id,
                    UsuarioId = usuarioCreado.Data.Id
                };

                var personaCreada = _personaManager.Crear(persona);

                if (!personaCreada.Success)
                {
                    throw new Exception("Error al crear la persona");
                }

                string query = "INSERT INTO Pacientes (PersonaId, ObraSocial, NroAfiliado) VALUES (@PersonaId, @ObraSocial, @NroAfiliado)";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@PersonaId", personaCreada.Data.Id),
                    new SqlParameter("@ObraSocial", entity.ObraSocial ?? ""),
                    new SqlParameter("@NroAfiliado", entity.NroAfiliado ?? ""),
                };

                var res = _DBManager.ExecuteQuery(query, parameters);
                entity.Id = res.GetId();

                _response.Ok(entity);
                return _response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response<Paciente> ObtenerPorId(int pacienteId)
        {
            string query = @"
            SELECT 
                p.Id AS Id, p.ObraSocial, p.NroAfiliado, p.PersonaId,
                per.Id AS PersonaId, per.Nombre, per.Apellido, per.Documento, 
                per.Telefono, per.FechaNacimiento, per.EmailPersonal, per.DireccionId, per.UsuarioId
            FROM Pacientes p
            INNER JOIN Personas per ON p.PersonaId = per.Id
            WHERE p.Id = @Id";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", pacienteId)
            };

            var res = _DBManager.ExecuteQuery(query, parameters);

            if (res.Rows.Count == 0)
            {
                _response.NotOk("Error al encontrar paciente");
                return _response;
            }

            var paciente = res.GetEntity<Paciente>();
            paciente.Direccion = _direccionManager.ObtenerPorId(paciente.DireccionId).Data;
            //paciente.HistoriaClinica = ObtenerHistoriaClinicaPorPacienteId(pacienteId).Data;

            _response.Ok(paciente);
            return _response;
        }

        public Response<List<Paciente>> ObtenerTodos()
        {
            string query = @"
        SELECT 
            p.Id AS PacienteId, p.ObraSocial, p.NroAfiliado,
            per.Id AS PersonaId, per.Nombre, per.Apellido, per.Documento, 
            per.Telefono, per.FechaNacimiento, per.EmailPersonal, per.DireccionId, per.UsuarioId
        FROM Pacientes p
        INNER JOIN Personas per ON p.PersonaId = per.Id";

            var res = _DBManager.ExecuteQuery(query);
            var response = new Response<List<Paciente>>();

            if (res.Rows.Count == 0)
            {
                _response.NotOk("No se encontraron pacientes en la base de datos.");
                return response;
            }

            var pacientes = new List<Paciente>();

            foreach (DataRow row in res.Rows)
            {
                var paciente = _mapper.MapFromRow(row);
                paciente.Direccion = _direccionManager.ObtenerPorId(paciente.DireccionId).Data;

                pacientes.Add(paciente);
            }

            response.Ok(pacientes);
            return response;
        }

        public Response<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Response<bool> Update(Paciente entity)
        {
            throw new NotImplementedException();
        }

        public Paciente ObtenerPacienteByUserId(int userId)
        {
            string query = @"
            SELECT 
                PAC.*,           
                PER.*,          
                DIR.*            
            FROM 
                Pacientes PAC
            LEFT JOIN 
                Personas PER ON PAC.PersonaId = PER.Id
            LEFT JOIN 
                Direcciones DIR ON DIR.Id = PER.DireccionId
            WHERE 
                PER.UsuarioId = @UserId";


            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId)
            };

            var res = _DBManager.ExecuteQuery(query, parameters);
            return res.GetEntity<Paciente>();
        }
            #endregion

            #region Private methods
            //private Response<HistoriaClinica> ObtenerHistoriaClinicaPorPacienteId(int pacienteId)
            //{
            //    string query = @"
            //    SELECT 
            //        hc.Id AS HistoriaClinicaId, hc.Detalle, hc.FechaCreacion, hc.PacienteId
            //    FROM HistoriaClinica hc
            //    WHERE hc.PacienteId = @PacienteId";

            //    SqlParameter[] parameters = new SqlParameter[]
            //    {
            //        new SqlParameter("@PacienteId", pacienteId)
            //    };

            //    var response = new Response<HistoriaClinica>();

            //    var res = _DBManager.ExecuteQuery(query, parameters);

            //    if (res.Rows.Count == 0)
            //    {
            //        response.NotOk("No se encontró una historia clínica para el paciente especificado.");
            //        return response;
            //    }

            //    var historiaClinica = res.GetEntity<HistoriaClinica>();

            //    response.Ok(historiaClinica);
            //    return response;
            //}

            #endregion
        }
}
