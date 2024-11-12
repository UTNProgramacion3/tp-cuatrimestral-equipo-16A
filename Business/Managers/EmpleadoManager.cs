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
        private readonly IMapper<Empleado> _mapper;
        #endregion

        #region Builder
        public EmpleadoManager(
            DBManager manager, 
            Response<Empleado> response, 
            IUsuarioManager usuarioManager, 
            IDireccionManager direccionManager, 
            IPersonaManager personaManager, 
            IMapper<Empleado> mapper) 
        { 
            _DBManager = manager;
            _response = response;
            _usuarioManager = usuarioManager;
            _direccionManager = direccionManager;
            _personaManager = personaManager;
            _mapper = mapper;
        }
        #endregion

        #region Public methods
        public Response<Empleado> Crear(Empleado entity)
        {
            try
            {
                var user = _usuarioManager.GenerarUsuario((Persona)entity, (int)RolesEnum.Empleado);
                var usuarioCreado = _usuarioManager.Crear(user);

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
                    Direccion = direccionCreada.Data,
                    UsuarioId = usuarioCreado.Data.Id,
                };

                var personaCreada = _personaManager.Crear(persona);

                if (!personaCreada.Success)
                {
                    throw new Exception("Error al crear la persona");
                }

                var query = "Insert into Empleados values(@PersonaId, @UsuarioId, @Legajo, @EmailCorporativo, @Posicion, @JornadaTrabajoId)";

                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@PersonaId", personaCreada.Data.Id),
                    new SqlParameter("@UsuarioId", usuarioCreado.Data.Id),
                    new SqlParameter("@Legajo", entity.Legajo),
                    new SqlParameter("@EmailCorporativo", entity.EmailCorporativo),
                    new SqlParameter("@Posicion", entity.Posicion),
                    new SqlParameter("@JornadaTrabajoId", entity.JornadaTrabajoId),
                };

                var res = _DBManager.ExecuteQuery(query, parameters);

                entity.Id = res.GetId();

                _response.Ok(entity);

                return _response;
            }
            catch(Exception ex)
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
