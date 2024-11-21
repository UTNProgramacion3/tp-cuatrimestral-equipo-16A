using Business.Interfaces;
using DataAccess;
using DataAccess.Extensions;
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
    public class DireccionManager : IDireccionManager
    {
        private readonly DBManager _DBManager;
        private Response<Direccion> _response;
        public DireccionManager(DBManager dbManager, Response<Direccion> response)
        {
            _DBManager = dbManager;
            _response = response;
        }

        public Response<Direccion> Crear(Direccion entity)
        {
            string queryDireccion = @"
                INSERT INTO Direcciones (Calle, Numero, Piso, Depto, Localidad, Provincia, CodigoPostal)
                VALUES (@Calle, @Numero, @Piso, @Departamento, @Localidad, @Provincia, @CodigoPostal);
                ";
            string retrieveData = "select * from direcciones where Calle=@Calle and Numero=@Numero and Localidad=@Localidad and Provincia=@Provincia";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Calle", entity.Calle),
                new SqlParameter("@Numero", entity.Numero),
                new SqlParameter("@Piso", entity.Piso ?? ""),
                new SqlParameter("@Departamento", entity.Depto ?? ""),
                new SqlParameter("@Localidad", entity.Localidad),
                new SqlParameter("@Provincia", entity.Provincia),
                new SqlParameter("@CodigoPostal", entity.CodigoPostal)
            };

            var res = _DBManager.ExecuteNonQueryAndGetData(queryDireccion, parameters, retrieveData);

            entity.Id = res.GetId();
            _response.Ok(entity);

            return _response;
        }

        public Response<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Response<Direccion> ObtenerPorId(int id)
        {
            string query = "Select * from Direcciones where Id = @Id";

            var res = _DBManager.ExecuteQuery(query, new SqlParameter[] { new SqlParameter("@Id", id) });

            _response.Ok(res.GetEntity<Direccion>());
            return _response;
        }

        public Response<List<Direccion>> ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        public Response<bool> Update(Direccion entity)
        {
            throw new NotImplementedException();
        }
    }
}
