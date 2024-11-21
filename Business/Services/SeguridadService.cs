﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using DataAccess;
using Domain.Entities;
using Domain.Enums;
using Domain.Response;
using Utils;

namespace Business.Services
{
    public class SeguridadService : ISeguridadService
    {
        private DBManager _dbManager;
        private Response<List<Permiso>> _response;
        private Mapper<Permiso> _mapper;

        public SeguridadService()
        {
            _dbManager = new DBManager();
            _response = new Response<List<Permiso>>();
            _mapper = new Mapper<Permiso>();
        }

        public Response<List<Permiso>> GetPermisosPorModulo(int moduloId)
        {
            string query = @"SELECT p.Id, p.Nombre, m.Id AS Modulo_Id, m.Nombre AS Modulo_Nombre
                            FROM Permisos p
                            JOIN Modulos m ON p.ModuloId = m.Id
                            WHERE m.Id = @ModuloId";
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@ModuloId", moduloId) };

            try
            {
                var res = _dbManager.ExecuteQuery(query, parameters);

                if(res.Rows.Count == 0)
                {
                    _response.NotOk($"Error al encontrar permisos del modulo {moduloId}");
                    return _response;
                }

                var permisos = _mapper.ListMapFromRow(res);

                if(permisos.Count > 0) 
                {
                    _response.Ok(permisos);
                }

            }catch (Exception ex) 
            {
                _response.NotOk(ex.Message);
                
            }

            return _response;
        }


        public Response<List<Permiso>> GetPermisosPorRol(int rolId)
        {
            string query = @"SELECT p.Id, p.Nombre, m.Id AS Modulo_Id, m.Nombre AS Modulo_Nombre
                             FROM Permisos p
                             JOIN Modulos m ON p.ModuloId = m.Id
                             JOIN PermisosRoles pr ON pr.PermisoId = p.Id
                             WHERE pr.RolId = @RolId";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@RolId", rolId)
            };

            RolesEnum rol = (RolesEnum)rolId;

            try
            {
                var res = _dbManager.ExecuteQuery(query, parameters);

                if(res.Rows.Count == 0)
                {
                    _response.NotOk($"Error al encontrar permisos del {rol.ToString()}.");
                    return _response;
                }

                var permisos = _mapper.ListMapFromRow(res);

                if(permisos.Count > 0)
                {
                    _response.Ok(permisos);
                }

            }catch (Exception ex)
            {
                _response.NotOk(ex.Message );
            }

            return _response;
        }

        public bool TienePermiso(int rolId, string permisoId)
        {
            string query = @"SELECT COUNT(*) 
                             FROM PermisosRoles pr
                             WHERE pr.RolId = @RolId AND pr.PermisoId = @permisoId";

            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@RolId", rolId),
                new SqlParameter("@permisoId", permisoId)
            };

            var res = (_dbManager.ExecuteScalar(query,parameters));

            return Convert.ToInt32(res) > 0;
        }
    }
}