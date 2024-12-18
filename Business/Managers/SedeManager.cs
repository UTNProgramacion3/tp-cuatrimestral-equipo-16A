﻿using Business.Dtos;
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
using System.Data.SqlClient;
using System.Security.Cryptography;
using DataAccess.Extensions;


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
            _response = new Response<SedeDto>();
        }
        public Response<SedeDto> Crear(SedeDto entity)
        {
            string query = @"Insert into Sedes (Nombre, DireccionId)
                           VALUES (@Nombre, @IdDireccion)";

            SqlParameter[]parameters = new SqlParameter[]
            {
               new SqlParameter(@"Nombre", entity.Sede.Nombre),
               new SqlParameter(@"IdDireccion", entity.Direccion.Id)
            };

            try
            {
                var res = _dbManager.ExecuteNonQuery(query, parameters);

                if(res == 0)
                {
                    return new Response<SedeDto>();
                }
                else
                {
                    _response.Ok(entity);
                    return _response;
                }
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

        public Response<Sede> ObeterSedeById(int id)
        {

            Response<Sede> response = new Response<Sede>();
            
            string query = @"
                SELECT Id, Nombre, DireccionId
                FROM Sedes SE
                WHERE SE.Id = @Id";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };

            var res = _dbManager.ExecuteQuery(query, parameters);

            if (res == null)
            {
                throw new Exception("No se encontró la sede con el ID proporcionado");
            }

            var sede = res.GetEntity<Sede>();

            response.Ok(sede);

            return response;
        }


        public Response<List<SedeDto>> ObtenerTodos()
        {
            string query = @"
                            SELECT
                            SE.Id AS Sede_Id,
                            SE.Nombre AS Sede_Nombre,
                            DI.Calle AS Direccion_Calle,
                            DI.Numero AS Direccion_Numero,
                            DI.Localidad AS Direccion_Localidad
                            FROM Sedes SE
                            LEFT JOIN Direcciones DI ON SE.DireccionId = DI.Id
                            WHERE SE.DireccionId = DI.Id;";

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

        public Sede Update(Sede entity)
        {
            string query = @"
                            UPDATE Sedes
                            Set Nombre = @Nombre,
                                DireccionId = @IdDireccion
                            Where Id = @Id
                            ";

            SqlParameter[] parameters = new SqlParameter[]
            {
               new SqlParameter("@Nombre", entity.Nombre),
               new SqlParameter("@IdDireccion", entity.DireccionId),
               new SqlParameter("@Id", entity.Id)
            };

            try
            {

                var res = _dbManager.ExecuteNonQuery(query, parameters);

                if (res == 0)
                {
                    return new Sede();
                }

                return entity;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
