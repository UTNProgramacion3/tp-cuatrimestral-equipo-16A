using Business.Interfaces;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using DataAccess;
using System.Data.SqlClient;
using DataAccess.Extensions;
using System.Data;
using Utils;

namespace Business.Managers
{
    public class EspecialidadManager : IEspecialidadManager
    {
        private readonly DBManager _DBManager;
        private Response<Especialidad> _response;
        private readonly Mapper<Especialidad> _mapper;

        public EspecialidadManager(DBManager dbManager, Response<Especialidad> response)
        {
            _DBManager = dbManager;
            _response = response;
            _mapper = new Mapper<Especialidad>();
        }


        public Response<Especialidad> Crear(Especialidad entity)
        {
            string query = @"Insert into Especialidades VALUES (@Nombre)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Nombre", entity.Nombre)
            };

            var res = _DBManager.ExecuteQuery(query, parameters);

            entity.Id = res.GetId();
            _response.Ok(entity);

            return _response;

        }

        public Response<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Response<Especialidad> ObtenerPorId(int id)
        {

            string query = "Select Id, Nombre From Especialidades Where Id = @Id";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };


            var res = _DBManager.ExecuteQuery(query, parameters);

            _response.Ok(res.GetEntity<Especialidad>());

            return _response;

        }

        public Response<List<Especialidad>> ObtenerTodos()
        {
            string query = "Select Id, Nombre From Especialidades";

          
            DataTable table = _DBManager.ExecuteQuery(query);

            Response<List<Especialidad>> response = new Response<List<Especialidad>>();

            response.Data = _mapper.ListMapFromRow(table);


            return response;

        }

        public Response<bool> Update(Especialidad entity)
        {

            string query = @"UPDATE Especialidades
                             SET
                             Nombre = @Nombre
                             Where Id = @Id";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter(@"Id", entity.Id),
                new SqlParameter(@"Nombre", entity.Nombre)
            };

            try
            {
                var res = _DBManager.ExecuteNonQuery(query, parameters);

                if (res == 0)
                {
                    return new Response<bool>();
                }

                return new Response<bool>();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
