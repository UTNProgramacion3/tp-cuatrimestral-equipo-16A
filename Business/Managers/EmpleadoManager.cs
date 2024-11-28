using Business.Dtos;
using Business.Interfaces;
using DataAccess;
using DataAccess.Extensions;
using Domain.Entities;
using Domain.Enums;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Utils.Interfaces;

namespace Business.Managers
{
    public class EmpleadoManager : IEmpleadoManager
    {
        #region Private properties
        private readonly DBManager _DBManager;
        private readonly Response<Empleado> _response;
        private readonly IUsuarioManager _usuarioManager;
        private readonly IDireccionManager _direccionManager;
        private readonly IPersonaManager _personaManager;
        private readonly IEmailManager _emailManager;
        private readonly IJornadaManager _jornadaManager;
        private readonly IMedicoManager _medicoManager;
        private readonly IMapper<Empleado> _mapper;
        private readonly string _VALIDAR_CON_EMAIL;
        #endregion

        #region Builder
        public EmpleadoManager(
            DBManager manager,
            Response<Empleado> response,
            IUsuarioManager usuarioManager,
            IDireccionManager direccionManager,
            IPersonaManager personaManager,
            IJornadaManager jornadaManager,
            IMedicoManager medicoManager,
            IEmailManager emailManager)
        {
            _DBManager = manager;
            _response = response;
            _usuarioManager = usuarioManager;
            _direccionManager = direccionManager;
            _personaManager = personaManager;
            _emailManager = emailManager;
            _jornadaManager = jornadaManager;
            _medicoManager = medicoManager;
            _mapper = new Mapper<Empleado>();
            _VALIDAR_CON_EMAIL = Environment.GetEnvironmentVariable("VALIDAR_CON_EMAIL");
        }



        #region Public methods
        public Response<Empleado> CrearNuevo(NuevoEmpleadoDto entity)
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

                var persona = new Persona()
                {
                    Apellido = entity.Apellido,
                    Nombre = entity.Nombre,
                    Documento = entity.Documento,
                    EmailPersonal = entity.EmailPersonal,
                    FechaNacimiento = entity.FechaNacimiento,
                    Telefono = entity.Telefono,
                    DireccionId = direccionCreada.Data.Id,
                    UsuarioId = usuarioCreado.Data.Id,
                };

                var personaCreada = _personaManager.Crear(persona);

                if (!personaCreada.Success)
                {
                    throw new Exception("Error al crear la persona");
                }

                JornadaTrabajo jornadaEmpleado = new JornadaTrabajo();
                var jornada = _jornadaManager.Crear(jornadaEmpleado);

                var query = "Insert into Empleados(Legajo, EmailCorporativo, CargoId, JornadaTrabajoId,PersonaId) values(@Legajo ,@EmailCorporativo,  @CargoId,@JornadaTrabajoId, @PersonaId )";

                string retrieveData = "select * from Empleados where Legajo = @Legajo";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@Legajo", entity.Legajo),
                    new SqlParameter("@EmailCorporativo", usuarioCreado.Data.Email),
                    new SqlParameter("@CargoId", entity.Posicion),
                    new SqlParameter("@JornadaTrabajoId", jornada.Data.Id),
                    new SqlParameter("@PersonaId", personaCreada.Data.Id),
                };

                var res = _DBManager.ExecuteNonQueryAndGetData(query, parameters, retrieveData);
                Empleado empleadoCreado = res.GetEntity<Empleado>();

                if (entity.Posicion == (int)PosicionEnum.Medico)
                {
                    MedicoDto nuevoMedico = new MedicoDto()
                    {
                        EmpleadoId = empleadoCreado.Id,
                        Matricula = entity.Matricula,
                        EspecialidadId = entity.EspecialidadId
                    };
                    _medicoManager.CrearMedico(nuevoMedico);
                }

                _response.Ok(empleadoCreado);

                return _response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Response<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Response<Empleado> ObtenerPorId(int empleadoId)
        {
            string query = "SELECT * FROM Empleados WHERE Id = @Id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", empleadoId)
            };

            var res = _DBManager.ExecuteQuery(query, parameters);

            if (res.Rows.Count == 0)
            {
                _response.NotOk("Error al encontrar empleado");
                return _response;
            }



            var empleado = _mapper.MapFromRow(res.Rows[0]);
            empleado.Direccion = _direccionManager.ObtenerPorId(empleado.DireccionId).Data;

            _response.Ok(empleado);

            return _response;
        }

        public Response<List<Empleado>> ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        public Response<bool> Update(Empleado entity)
        {
            throw new NotImplementedException();
        }

        public Response<Empleado> ActualizarEmpleado(NuevoEmpleadoDto entity)
        {
            try
            {
                //Actualizar Dirección
                var direccionActualizada = _direccionManager.Actualizar(entity.Direccion);

                if (!direccionActualizada.Success)
                {
                    throw new Exception("Error al actualizar la dirección");
                }

                // Actualizar Persona
                var persona = new Persona()
                {
                    Id = entity.Id,
                    Apellido = entity.Apellido,
                    Nombre = entity.Nombre,
                    Documento = entity.Documento,
                    EmailPersonal = entity.EmailPersonal,
                    FechaNacimiento = entity.FechaNacimiento,
                    Telefono = entity.Telefono,
                    DireccionId = entity.DireccionId,
                    UsuarioId = entity.UsuarioId,
                };

                var personaActualizada = _personaManager.Actualizar(persona);

                if (!personaActualizada.Success)
                {
                    throw new Exception("Error al actualizar la persona");
                }

                var query = "UPDATE Empleados " +
                            "SET Legajo = @Legajo, EmailCorporativo = @EmailCorporativo, CargoId = @CargoId, JornadaTrabajoId = @JornadaTrabajoId " +
                            "WHERE Id = @EmpleadoId";

                var retrieveData = "SELECT * FROM Empleados WHERE Id = @EmpleadoId";
                
                var parameters = new SqlParameter[]
                {
                new SqlParameter("@EmpleadoId", entity.Id),
                new SqlParameter("@Legajo", entity.Legajo),
                new SqlParameter("@EmailCorporativo", entity.EmailCorporativo),
                new SqlParameter("@CargoId", entity.Posicion),
                new SqlParameter("@JornadaTrabajoId", entity.JornadaTrabajoId)
                };

                var res = _DBManager.ExecuteNonQueryAndGetData(query, parameters, retrieveData);
                Empleado empleadoActualizado = res.GetEntity<Empleado>();

                // Actualizar Médico (si aplica)
                if (entity.RolId == (int)RolesEnum.Medico)
                {

                    MedicoDto medicoActualizado = new MedicoDto()
                    {
                        EmpleadoId = empleadoActualizado.Id,
                        Matricula = entity.Matricula,
                        EspecialidadId = entity.EspecialidadId,
                    };
                    _medicoManager.ActualizarMedico(medicoActualizado);
                }

                _response.Ok(empleadoActualizado);
                return _response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Response<Empleado> Crear(Empleado entity)
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion

        #region Private methods
        #endregion
    }
}
