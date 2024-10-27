using Business.Interfaces;
using DataAccess;
using Domain.Entities;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using Utils;


namespace Business.Managers
{
    public class UsuarioManager : IUsuarioManager
    {
        private readonly DBManager _dbManager;
        private readonly SessionManager _sessionManager;
        private readonly Mapper<Usuario> _mapper;

        public UsuarioManager()
        {
            _dbManager = new DBManager();
            _sessionManager = new SessionManager();
            _mapper = new Mapper<Usuario>();
        }

        public Response<Usuario> Crear(Usuario entity)
        {
            string query = @"INSERT INTO USUARIOS (email, passwordhash, idrol, activo)
                             VALUES (@email, @passwordhash, @idrol, @activo)";

            //Hasheamos la password antes de guardarla
            entity.Passwordhash = PasswordHasher.HashPassword(entity.Passwordhash);

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@email", entity.Email),
                new SqlParameter("@passwordhash", entity.Passwordhash),
                new SqlParameter("@idrol", entity.Rol.Id),
                new SqlParameter("@activo", entity.Activo)

            };

            var response = new Response<Usuario>();

            try
            {
                response.Success = Convert.ToBoolean(_dbManager.ExecuteNonQuery(query, parameters));

                if (response.Success)
                {
                    response.Data = entity;
                    response.Message = "Usuario creado con exito.";
                }
                else
                {
                    response.Data = entity;
                    response.Message = "Error, el usuario no puede ser creado";
                }

            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Success = false;
                response.Message = $"Error al Crear Usuario: {ex.Message}";
            }
            return response;
        }

        public Response<bool> Eliminar(int id)
        {
            string query = @"update USUARIOS
                            SET activo = 0
                            WHERE id = @id;
                            ";
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id",id) 
                };
            Response<bool> response = new Response<bool>();

            try
            {
                response.Success = Convert.ToBoolean(_dbManager.ExecuteNonQuery(query, parameters));
                if (response.Success)
                {
                    response.Data = true;
                    response.Message = "Usuario Eliminado fisicamente.";
                }
                else
                {
                    response.Data = false;
                    response.Message = "Error, el usuario no pudo ser eliminado";
                }

            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Success = false;
                response.Message = $"Error al Eliminar Usuario: {ex.Message}";
            }
            return response;
        }


        public Response<bool> LogIn(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Response<bool> LogOut(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Response<Usuario> ObtenerPorEmail(string email)
        {
            string query = @"Select 
	                            U.id,
	                            U.email,
	                            U.passwordhash,
	                            U.fecha_creacion,
	                            U.idrol as Rol_Id,
	                            R.nombre as Rol_Nombre,
	                            R.activo as Rol_Activo,
	                            U.img_perfil,
	                            U.activo
                            From USUARIOS U
                            Left Join ROLES R ON U.idrol = R.id
                            Where U.email = @email;";
            SqlParameter[] parametrs = new SqlParameter[]
            {
                new SqlParameter("@email", email)
            };

            var response = new Response<Usuario>();

            try
            {
                DataTable res = _dbManager.ExecuteQuery(query, parametrs);

                if (res.Rows.Count == 0)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = $"No existe una cuenta con el Email ingresado.";

                    return response;
                }

                response.Data = _mapper.MapFromRow(res.Rows[0]);

                if (response.Data != null)
                {
                    response.Success = true;
                    response.Message = $"Usuario {response.Data.Email} encontrado";
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = $"Error de mapeo";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Success = false;
                response.Message = $"Error {ex.Message}";
                return response;
            }
        }

        public Response<Usuario> ObtenerPorId(int id)
        {
            string query = @"Select 
	                            U.id,
	                            U.email,
	                            U.passwordhash,
	                            U.fecha_creacion,
	                            U.idrol as Rol_Id,
	                            R.nombre as Rol_Nombre,
	                            R.activo as Rol_Activo,
	                            U.img_perfil,
	                            U.activo
                            From USUARIOS U
                            Left Join ROLES R ON U.idrol = R.id
                            Where U.id = @id;";
            SqlParameter[] parametrs = new SqlParameter[] 
            { 
                new SqlParameter("@id", id)
            };

            var response = new Response<Usuario>();

            try
            {
                DataTable res = _dbManager.ExecuteQuery(query, parametrs);

                if (res.Rows.Count == 0)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = $"Usuario con id {id} no existe.";

                    return response;
                }

                response.Data = _mapper.MapFromRow(res.Rows[0]);

                if (response.Data != null)
                {
                    response.Success = true;
                    response.Message = $"Usuario {response.Data.Email} encontrado";
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = $"Error de mapeo";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Success = false;
                response.Message = $"Error {ex.Message}";
                return response;
            } 
        }

        public Response<List<Usuario>> ObtenerTodos()
        {
            string query = @"Select 
	                            U.id,
	                            U.email,
	                            U.passwordhash,
	                            U.fecha_creacion,
	                            U.idrol as Rol_Id,
	                            R.nombre as Rol_Nombre,
	                            R.activo as Rol_Activo,
	                            U.img_perfil,
	                            U.activo
                            From USUARIOS U
                            Left Join ROLES R ON U.idrol = R.id";
            Response<List<Usuario>> response = new Response<List<Usuario>>();

            try
            {
                DataTable table = _dbManager.ExecuteQuery(query);
                response.Data = _mapper.ListMapFromRow(table);

                if(response.Data != null && response.Data.Count != 0)
                {
                    response.Success = true;
                    response.Message = "Lista de usuarios obtenida.";
                }else
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "No se pudo obtener la lista de usuarios.";
                }
            }catch (Exception ex)
            {
                response.Data = null;
                response.Success = false;
                response.Message = $"Error al querer obtener lista de usuarios: {ex.Message}";
            }

            return response; 
        }

        public Response<bool> Update(Usuario entity)
        {
            string query = @"Update USUARIOS
                            Set   email = @email,
	                              passwordhash = @passwordhash,
	                              idrol = @idrol,
	                              img_perfil = @img_perfil,
	                              activo = @activo
                            Where id = @id";
            //Hasheamos la password antes de guardarla
            entity.Passwordhash = PasswordHasher.HashPassword(entity.Passwordhash);
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@email", entity.Email),
                    new SqlParameter("@passwordhash", entity.Passwordhash),
                    new SqlParameter("@idrol", entity.Rol.Id),
                    new SqlParameter("@img_perfil", string.IsNullOrEmpty(entity.ImagenPerfil) ? null : entity.ImagenPerfil),
                    new SqlParameter("@activo", entity.Activo),
                    new SqlParameter("@id", entity.Id)
                };

            Response<bool> response = new Response<bool>();

            try
            {
                response.Success = Convert.ToBoolean(_dbManager.ExecuteNonQuery(query, parameters));

                if(response.Success == false)
                {
                    response.Data = false;
                    response.Success = false;
                    response.Message = "No se encuentra este usuario a editar.";
                }else
                {
                    response.Data = true;
                    response.Success = true;
                    response.Message = $"Usuario {entity.Email} editado correctamente.";
                }

            }catch (Exception ex)
            {
                response.Data = false;
                response.Success = false;
                response.Message = $"Error al editar usuario: {ex.Message}";
            }

            return response;
        }

        public Response<bool> VerificarPassword(string password, string hashedpassword)
        {
            Response<bool> response = new Response<bool>();

            return response;
        }
    }
}
