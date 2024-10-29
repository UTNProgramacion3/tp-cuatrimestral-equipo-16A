using Business.Interfaces;
using DataAccess;
using Domain.Entities;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class PersonaManager : IPersonaManager
    {
        private readonly Response<Persona> _response;
        private readonly DBManager _DBManager;

        public PersonaManager(Response<Persona> response, DBManager dbManager)
        {
            _response = response;
            _DBManager = dbManager;
        }
        public Response<Persona> Crear(Persona entity)
        {
            var query = "Insert into Personas values(@Nombre, @Apellido, @Documento, @FechaNacimiento, @EmailPersonal, @Telefono, @DireccionId, @Localidad, @Provincia, @CodigoPostal, @UsuarioId)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Nombre", entity.Nombre),
                new SqlParameter("@Apellido", entity.Apellido),
                new SqlParameter("@Documento", entity.Documento),
                new SqlParameter("@FechaNacimiento", entity.FechaNacimiento),
                new SqlParameter("@EmailPersonal", entity.EmailPersonal),
                new SqlParameter("@Telefono", entity.Telefono),
                new SqlParameter("@DireccionId", entity.Direccion.Id),
            };

            var res = _DBManager.ExecuteQuery(query, parameters);
            
            _response.Ok(entity);
            return _response;
        }

        public Response<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Response<Persona> ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Response<List<Persona>> ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        public Response<bool> Update(Persona entity)
        {
            throw new NotImplementedException();
        }
    }
}
