using Business.Dtos;
using Business.Interfaces;
using DataAccess;
using Domain.Entities;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Interfaces;
using Utils;
using System.Data;

namespace Business.Managers
{
    public class SedeManager : ISedeManager
    {
        private DBManager _dbManager;
        private Mapper<SedeDto> _mapper;
        private Response<SedeDto> _response;

        public SedeManager()
        {
            _dbManager = new DBManager();
            _mapper = new Mapper<SedeDto>();
        }
        public Response<Sede> Crear(Sede entity)
        {
            throw new NotImplementedException();
        }

        public Response<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Response<Sede> ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }


        public Response<List<SedeDto>> ObtenerTodos()
        {
            string query = @"
                            SELECT
	                        SE.Nombre AS Sede_Nombre,
	                        DI.Calle AS Direccion_Calle,
	                        DI.Numero AS Direccion_Numero,
	                        DI.Localidad AS Direccion_Localidad
                            FROM Sedes SE
                            INNER JOIN Direcciones DI ON SE.DireccionId = SE.Id
                            Where SE.DireccionId = DI.Id";

            try
            {
                Response<List<SedeDto>> response = new Response<List<SedeDto>>();

                DataTable res = _dbManager.ExecuteQuery(query);

                if (res.Rows.Count == 0)
                {
                    return new Response<List<SedeDto>>();
                }


                response.Data = _mapper.ListMapFromRow(res);


                return response;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public Response<bool> Update(Sede entity)
        {
            throw new NotImplementedException();
        }
    }
}
