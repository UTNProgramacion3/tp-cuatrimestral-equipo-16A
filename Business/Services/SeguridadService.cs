using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Business.Dtos;
using Business.Interfaces;
using DataAccess;
using DataAccess.Extensions;
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

        public Response<List<Modulo>> GetModulos()
        {
            List<Modulo> modulos = new List<Modulo>();
            Response<List<Modulo>> response = new Response<List<Modulo>>(); 

            string query = @"
                            SELECT M.Id, M.Nombre, P.Id AS Permiso_Id, P.Nombre AS Permiso_Nombre
                            FROM Modulos M
                            LEFT JOIN Permisos P ON M.Id = P.ModuloId";

            try
            {
                var dt = _dbManager.ExecuteQuery(query);
                modulos = MapearModulosConPermisos(dt);

                if (modulos != null)
                {
                    response.Ok(modulos);
                }
                else
                {
                    response.NotOk("Error al querer mapear lista de modulos");
                }

            }
            catch (Exception ex)
            {
                response.NotOk(ex.Message);
            }

            return response;

        }

        private List<Modulo> MapearModulosConPermisos(DataTable dt)
        {
            var modulos = new List<Modulo>();

            foreach (DataRow row in dt.Rows)
            {
                int moduloId = Convert.ToInt32(row["Id"]);
                string moduloNombre = row["Nombre"].ToString();

                var modulo = modulos.FirstOrDefault(m => m.Id == moduloId);

                if (modulo == null)
                {
                    modulo = new Modulo
                    {
                        Id = moduloId,
                        Nombre = moduloNombre
                    };

                    modulos.Add(modulo);
                }

                if (!row.IsNull("Permiso_Id"))
                {
                    var permiso = new Permiso
                    {
                        Id = Convert.ToInt32(row["Permiso_Id"]),
                        Nombre = row["Permiso_Nombre"].ToString()
                    };
                    modulo.Permisos.Add(permiso);
                }

            }
                return modulos;
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

        public void InhabilitarToken(string token)
        {
            string query = "UPDATE EmailValidaciones set Activo = 0 where Token = @token";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@token", token)
            };

            var res = _dbManager.ExecuteQuery(query, parameters);
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

        public bool AgregarPermiso(string permiso, int moduloId)
        {
            string query = "INSERT INTO Permisos (Nombre, ModuloId) VALUES (@Nombre, @ModuloId)";
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Nombre", permiso),
                    new SqlParameter("@ModuloId", moduloId)
                };

            try
            {
                var res = _dbManager.ExecuteNonQuery(query, parameters);
                return res > 0;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool EditarPermiso(int permisoId, string nombre)
        {
            string query = @"UPDATE Permisos
                            SET Nombre = @nombre
                            WHERE Id = @PermisoId;";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@nombre", nombre),
                new SqlParameter("@permisoId", permisoId)
            };

            try
            {
                var res = _dbManager.ExecuteNonQuery(query, parameters);
                return res > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public Response<List<Rol>> GetRolesConPermisos()
        {
            string query = @"Select * From Roles";
            Response<List<Rol>> response = new Response<List<Rol>>();
            List<Rol> roles = new List<Rol>();
            Mapper<Rol> mapper = new Mapper<Rol>();

            try
            {
                var dt = _dbManager.ExecuteQuery(query);

                if (dt.Rows.Count == 0)
                {
                    response.NotOk("Error al traer roles.");
                    return response;
                }

                roles = mapper.ListMapFromRow(dt);

                if(roles.Count == 0)
                {
                    response.NotOk("Error al mapear roles");
                }

                foreach(Rol rol in roles)
                {
                    var res = GetPermisosPorRol(rol.Id);

                    if(res.Success)
                    {
                        rol.Permisos = res.Data;
                    }
                }
                response.Ok(roles);

            }catch (Exception ex)
            {
                response.NotOk(ex.Message);
                return response;
            }

            return response;
        }

        public Response<List<Permiso>> GetPermisos()
        {
            string query = "Select * From Permisos";
            try
            {
                var res = _dbManager.ExecuteQuery(query);

                if(res.Rows.Count == 0)
                {
                    _response.NotOk("Error al traer permisos.");

                    return _response;
                }

                var permisos = _mapper.ListMapFromRow(res);

                if(permisos.Count == 0)
                {
                    _response.NotOk("Error al mapear");
                    return _response;
                }

                _response.Ok(permisos);
            }
            catch (Exception ex)
            {
                _response.NotOk(ex.Message);
                return _response;
            }

            return _response;

        }

        public bool AsignarPermisoARol(int permisoId, int rolId)
        {
            string query = @"Insert Into PermisosRoles (PermisoId, RolId) Values (@permisoId, @rolId)";

            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@permisoId", permisoId),
                    new SqlParameter("@rolId", rolId)
                };

            try
            {
                var res = _dbManager.ExecuteNonQuery(query, parameters);

                return res > 0;

            }catch (Exception ex)
            {
                return false;
            }
        }

        public bool EliminarPermisoDeRol(int permisoId, int rolId)
        {
            string query = @"DELETE FROM PermisosRoles
                             WHERE PermisoId = @permisoId AND RolId = @rolId";

            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@permisoId", permisoId),
                    new SqlParameter("@rolId", rolId)
                };

            try
            {
                var res = _dbManager.ExecuteNonQuery(query, parameters);

                return res > 0; 

            }catch (Exception ex)
            {
                return false;
            }
                
        }

        public bool PermitirAccesoAModulo(int rol, int[] roles)
        {
            return roles.Contains(rol);
        }

        //public void VerificarTokensVencidos()
        //{
        //    try
        //    {
        //    string query = "SELECT * FROM EmailValidaciones where TiempoExpiracion <= GETDATE() AND Activo = 1";
        //    var res = _dbManager.ExecuteQuery(query);
        //    string update = "UPDATE EmailValidaciones set Activo = 0 where token = @token";

        //    //var tokens = res.GetEntity<List<EmailValidationDto>>();
        //    //    if (tokens.Any())
        //    //    {
        //    //        foreach(var token in tokens)
        //    //        {
        //    //            SqlParameter[] parameters = new SqlParameter[] {
        //    //            new SqlParameter("@token", token.Token)
        //    //            };

        //    //            var result = _dbManager.ExecuteQuery(query,parameters);
        //    //        }

        //    //    }

        //    }catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
