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
        private readonly IMapper<Empleado> _mapper;
        #endregion

        #region Builder
        public EmpleadoManager(
            DBManager manager,
            Response<Empleado> response,
            IUsuarioManager usuarioManager,
            IDireccionManager direccionManager,
            IPersonaManager personaManager,
            IEmailManager emailManager)
        {
            _DBManager = manager;
            _response = response;
            _usuarioManager = usuarioManager;
            _direccionManager = direccionManager;
            _personaManager = personaManager;
            _emailManager = emailManager;
            _mapper = new Mapper<Empleado>();
        }
        #endregion

        #region Public methods
        public Response<Empleado> Crear(Empleado entity)
        {
            try
            {
                var user = _usuarioManager.GenerarUsuario((Persona)entity, entity.RolId);
                var usuarioCreado = _usuarioManager.Crear(user);
                var nombreUsuario = $"{entity.Apellido}, {entity.Nombre}";
                _emailManager.EnviarMailValidacionNuevaCuenta(entity.EmailPersonal, usuarioCreado.Data.Id, nombreUsuario);

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

                //var query = "Insert into Empleados(Legajo, EmailCorporativo, CargoId, JornadaTrabajoId,PersonaId) values(@Legajo ,@EmailCorporativo,  @CargoId,@PersonaId, @JornadaTrabajoId)";
                //string retrieveData = "select * from Empleados where Legajo = @Legajo";

                //var parameters = new SqlParameter[]
                //{
                //    new SqlParameter("@Legajo", entity.Legajo),
                //    new SqlParameter("@EmailCorporativo", usuarioCreado.Data.Email),
                //    new SqlParameter("@CargoId", 2),
                //    new SqlParameter("@JornadaTrabajoId", entity.JornadaTrabajoId),
                //    new SqlParameter("@PersonaId", personaCreada.Data.Id),

                //};

                //var res = _DBManager.ExecuteNonQueryAndGetData(query, parameters, retrieveData);

                //entity.Id = res.GetId(isNewId: true);

                _response.Ok(entity);

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
        #endregion

        #region Private methods
        #endregion
    }
}
