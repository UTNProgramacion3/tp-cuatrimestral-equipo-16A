using Business.Dtos;
using Business.Interfaces;
using DataAccess;
using DataAccess.Extensions;
using Domain.Entities;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

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
            var query = "Insert into Personas(Nombre, Apellido, Documento, EmailPersonal, Telefono, FechaNacimiento, DireccionId, UsuarioId) values(@Nombre, @Apellido, @Documento, @EmailPersonal, @Telefono,@FechaNacimiento, @DireccionId, @UsuarioId)";
            string retrieveData = "select * from Personas where UsuarioId = @UsuarioId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Nombre", entity.Nombre),
                new SqlParameter("@Apellido", entity.Apellido),
                new SqlParameter("@Documento", entity.Documento),
                new SqlParameter("@Telefono", entity.Telefono),
                new SqlParameter("@FechaNacimiento", entity.FechaNacimiento),
                new SqlParameter("@EmailPersonal", entity.EmailPersonal),
                new SqlParameter("@DireccionId", entity.DireccionId),
                new SqlParameter("@UsuarioId", entity.UsuarioId),
            };

            var res = _DBManager.ExecuteNonQueryAndGetData(query, parameters, retrieveData);

            entity.Id = res.GetId();

            if (res == null)
            {
                throw new Exception("Hubo un error al crear empleado");
            }
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

        public Response<PersonaDto> ObtenerPorUsuario(int id)
        {
            string query = @"SELECT * FROM Personas
                             WHERE UsuarioId = @UsuarioId;";

            SqlParameter[] parameters = new SqlParameter[] 
            {   
                new SqlParameter("@UsuarioId", id)
            };
            Mapper<PersonaDto> mapper = new Mapper<PersonaDto>();
            Response<PersonaDto> response = new Response<PersonaDto>();
            try
            {
                var res = _DBManager.ExecuteQuery(query, parameters);

                if (res.Rows.Count == 0)
                {
                    response.NotOk("Error al buscar Persona");
                    return response;
                }

                var persona = mapper.MapFromRow(res.Rows[0]);

                if (persona != null)
                {
                    response.Ok(persona);
                }
            }
            catch (Exception ex)
            {
                response.NotOk(ex.Message);
            }

            return response;
        }

        public bool EditarPersona(string nombre, string apellido, string documento, string telefono, string fechanacimiento, string emailpersonal, int personaId)
        {
            string query = @"UPDATE Personas
                            SET
                                Nombre = @Nombre,
                                Apellido = @Apellido,
                                Documento = @Documento,
                                Telefono = @Telefono,
                                FechaNacimiento = @FechaNacimiento,
                                EmailPersonal = @EmailPersonal
                            WHERE Id = @PersonaId";

            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Nombre", nombre),
                    new SqlParameter("@Apellido", apellido),
                    new SqlParameter("@Documento", documento),
                    new SqlParameter("@Telefono", telefono),
                    new SqlParameter("@FechaNacimiento", fechanacimiento),
                    new SqlParameter("@EmailPersonal", emailpersonal),
                    new SqlParameter("@PersonaId", personaId)
                };

            try
            {
                var res = _DBManager.ExecuteNonQuery(query, parameters);

                return res > 0;

            }
            catch (Exception ex) { return false; }
        }
    }
}
