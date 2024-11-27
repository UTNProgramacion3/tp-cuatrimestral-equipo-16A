using Business.Dtos;
using Business.Interfaces;
using DataAccess;
using DataAccess.Extensions;
using Domain.Entities;
using Domain.Enums;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utils;


namespace Business.Managers
{
    public class UsuarioManager : IUsuarioManager
    {
        private readonly DBManager _dbManager;
        private readonly SessionManager _sessionManager;
        private readonly Mapper<Usuario> _mapper;
        private Response<Usuario> _response;

        public UsuarioManager()
        {
            _dbManager = new DBManager();
            _sessionManager = new SessionManager();
            _mapper = new Mapper<Usuario>();
            _response = new Response<Usuario>();
        }

        public Response<Usuario> Crear(Usuario entity)
        {
            string query = @"INSERT INTO USUARIOS (Email, Passwordhash, RolId, Activo)
                             VALUES (@email, @passwordhash, @rolId, @activo)";

            string retrieveData = @"SELECT * FROM USUARIOS WHERE Email = @email";

            entity.Passwordhash = PasswordHasher.HashPassword(entity.Passwordhash);

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@email", entity.Email),
                new SqlParameter("@passwordhash", entity.Passwordhash),
                new SqlParameter("@rolId", entity.Rol.Id),
                new SqlParameter("@activo", entity.Activo)

            };

            var response = new Response<Usuario>();


            var res = _dbManager.ExecuteNonQueryAndGetData(query, parameters, retrieveData);
            var usuario = res.GetEntity<Usuario>(create:true);
            response.Ok(usuario);
                    
            return response;
        }

        public Response<bool> Eliminar(int id)
        {
            string query = @"update USUARIOS
                            SET Activo = 0
                            WHERE Id = @id;
                            ";
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@id",id)
                };
            Response<bool> response = new Response<bool>();

            try
            {
                var res = Convert.ToBoolean(_dbManager.ExecuteNonQuery(query, parameters));
                if (res == true)
                {
                    response.Ok(true, "Usuario desactivado.");
                }
                else
                {
                    response.NotOk("Error, el usuario no pudo ser eliminado");
                }

            }
            catch (Exception ex)
            {
                response.NotOk(ex.Message);
            }
            return response;
        }


        public Response<bool> LogIn(Usuario usuario)
        {
            Response<bool> response = new Response<bool>();

            try
            {

                _response = ObtenerPorEmail(usuario.Email);

                if (_response.Success)
                {
                    var res = VerificarPassword(usuario.Passwordhash, _response.Data.Passwordhash);
                    if (res == true)
                    {
                        _sessionManager.SetSessionValue("UserLogueado", _response.Data);
                        response.Ok(true, "Usuario logeado correctamente.");
                    }
                    else
                    {
                        response.NotOk("Contraseña incorrecta.");
                    }
                }
                else
                {
                    response.NotOk("Email no encontrado.");
                }
            }
            catch (Exception ex)
            {
                response.NotOk($"Error al loguear usuario: {ex.Message}");
            }
            return response;
        }

        public void LogOut(Usuario usuario)
        {
            _sessionManager.RemoveSessionValue("UserLogueado");
        }

        public List<UsuarioBasicoDto> ObtenerUsuariosDataBasica()
        {
            string query = @"select Usuarios.*, PER.Nombre + ' ' + PER.Apellido NombreCompleto, R.Nombre Rol, R.Id RolId from Usuarios
            left join Personas PER on PER.UsuarioId = Usuarios.Id
            left join Roles R ON R.Id = Usuarios.RolId";

            var res = _dbManager.ExecuteQuery(query);

            List<UsuarioBasicoDto> usuarios = new List<UsuarioBasicoDto>();

            var userMapper = new Mapper<UsuarioBasicoDto>();
            foreach (DataRow row in res.Rows)
            {
                var mappedUser = userMapper.MapFromRow(row);
                usuarios.Add(mappedUser);
            }

            return usuarios;
        }

        public Response<Usuario> ObtenerPorEmail(string email)
        {
            string query = @"Select 
	                            U.Id,
	                            U.Email,
	                            U.Passwordhash,
	                            U.FechaCreacion,
	                            U.RolId as Rol_Id,
	                            R.Nombre as Rol_Nombre,
	                            R.Activo as Rol_Activo,
	                            U.ImagenPerfil,
	                            U.Activo
                            From USUARIOS U
                            Left Join ROLES R ON U.RolId = R.Id
                            Where U.Email = @email;";
            SqlParameter[] parametrs = new SqlParameter[]
            {
                new SqlParameter("@email", email)
            };

            try
            {
                var res = _dbManager.ExecuteQuery(query, parametrs);

                if (res.Rows.Count == 0)
                {
                    _response.NotOk("Usuario no encontrado.");

                    return _response;
                }

                var usuario = _mapper.MapFromRow(res.Rows[0]);

                if (usuario != null)
                {
                    _response.Ok(usuario, "Usuario encontrado.");
                    return _response;
                }
                else
                {
                    _response.NotOk("Error de mapeo");
                    return _response;
                }
            }
            catch (Exception ex)
            {
                _response.NotOk(ex.Message);
                return _response;
            }
        }

        public Response<Usuario> ObtenerPorId(int id)
        {
            string query = @"Select 
	                            U.Id,
	                            U.Email,
	                            U.Passwordhash,
	                            U.FechaCreacion,
	                            U.RolId as Rol_Id,
	                            R.Nombre as Rol_Nombre,
	                            R.Activo as Rol_Activo,
	                            U.ImagenPerfil,
	                            U.Activo
                            From USUARIOS U
                            Left Join ROLES R ON U.RolId = R.Id
                            Where U.Id = @id;";
            SqlParameter[] parametrs = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };

            try
            {
                var res = _dbManager.ExecuteQuery(query, parametrs);

                if (res.Rows.Count == 0)
                {
                    _response.NotOk($"Usuario con id {id} no encontrado.");
                    return _response;
                }

                var usuario = _mapper.MapFromRow(res.Rows[0]);

                if (usuario != null)
                {
                    _response.Ok(usuario, "Usuario encontrado.");
                    return _response;
                }
                else
                {
                    _response.NotOk($"Usuario con id {id} no encontrado.");
                    return _response;
                }
            }
            catch (Exception ex)
            {
                _response.NotOk(ex.Message);
                return _response;
            }
        }

        public Response<List<Usuario>> ObtenerTodos()
        {
            string query = @"Select 
	                            U.Id,
	                            U.Email,
	                            U.Passwordhash,
	                            U.FechaCreacion,
	                            U.RolId as Rol_Id,
	                            R.Nombre as Rol_Nombre,
	                            R.Activo as Rol_Activo,
	                            U.ImagenPerfil,
	                            U.Activo
                            From USUARIOS U
                            Left Join ROLES R ON U.RolId = R.Id";
            Response<List<Usuario>> response = new Response<List<Usuario>>();

            try
            {
                DataTable table = _dbManager.ExecuteQuery(query);
                var lista = _mapper.ListMapFromRow(table);

                if (lista.Count != 0)
                {
                    response.Ok(lista);
                }
                else
                {
                    response.NotOk("Error al traer lista de usuarios.");
                }
            }
            catch (Exception ex)
            {
                response.NotOk(ex.Message);
            }

            return response;
        }

        public Response<bool> Update(Usuario entity)
        {
            string query = @"Update USUARIOS
                            Set   Email = @email,
	                              PasswordHash = @passwordhash,
	                              ImagenPerfil = @img_perfil,
                                    Activo = @activo
                            Where Id = @id";
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@email", entity.Email),
                    new SqlParameter("@passwordhash", entity.Passwordhash),
                    new SqlParameter("@img_perfil", entity.ImagenPerfil ?? ""),
                    new SqlParameter("@id", entity.Id),
                    new SqlParameter("@activo", entity.Activo)
                };

            Response<bool> response = new Response<bool>();

            try
            {
                var res = Convert.ToBoolean(_dbManager.ExecuteNonQuery(query, parameters));

                if (res == false)
                {
                    response.NotOk("No se pudo editar el usuario");
                }
                else
                {
                    response.Ok(res, "Usuario editado correctamente.");
                }

            }
            catch (Exception ex)
            {
                response.NotOk(ex.Message);
            }

            return response;
        }

        public bool VerificarPassword(string password, string hashedpassword)
        {
            return PasswordHasher.VerifyPassword(password, hashedpassword);
           
        }  

        public bool ExisteMail(string email)
        {
            bool result = false;
            string query = @"select count(*) from usuarios 
                            where email = @email;";
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@email", email)
                };

            try
            {
                result = Convert.ToBoolean(_dbManager.ExecuteScalar(query, parameters));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);    
            }

            return result;
        }
        public Usuario GenerarUsuario(Persona persona, int tipoUsuario)
        {
            var email = tipoUsuario == (int)RolesEnum.Empleado ? persona.CrearEmailCorporativo() : persona.EmailPersonal; //Añadir validación mail existente.

            Usuario usuario = new Usuario
            {
                Email = email,
                Passwordhash = persona.Documento.ToString(),
                FechaCreacion = DateTime.Now,
                Rol = AsignarRol(persona, tipoUsuario),
                ImagenPerfil = "",
                Activo = false,
            };

            return usuario;
        }

        public Usuario ValidarToken(string token)
        {
            try
            {
                string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var parts = decodedToken.Split('|');

                if (parts.Length == 2 && DateTime.TryParse(parts[1], out DateTime expirationDate) && expirationDate > DateTime.Now)
                {
                    var email = parts[0];

                    string query = @"
                        SELECT U.* FROM Personas P
                        LEFT JOIN Usuarios U on P.UsuarioId = U.Id
                        LEFT JOIN Emailvalidaciones VAL on VAL.UsuarioId = U.Id
                        where P.EmailPersonal = 'escuderopablo.m@gmail.com' and VAL.TiempoExpiracion >= GETDATE() AND VAL.Token = @token and VAL.Activo = 1";

                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@email", email),
                        new SqlParameter("@token", token)
                    };

                    var res = _dbManager.ExecuteQuery(query, parameters);
                    return res.GetEntity<Usuario>();
                }
                return new Usuario();
            }
            catch (FormatException)
            {
                return new Usuario();
            }
        }

        public Usuario ActivarUsuario(Usuario usuario, string password)
        {
            usuario.Activo = true;
            usuario.Passwordhash = password;

            return usuario;
        }

        public Response<bool> CambiarPassword(string newPass, int userId)
        {
            string query = @"Update USUARIOS
                            Set   PasswordHash = @newPass
                            Where Id = @id";
            string hashedPass = PasswordHasher.HashPassword(newPass);
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter ("@newPass", hashedPass),
                    new SqlParameter ("@id", userId)
                };

            Response<bool> response = new Response<bool>();

            try
            {
                var res = Convert.ToBoolean(_dbManager.ExecuteNonQuery(query, parameters));

                if (res == false)
                {
                    response.NotOk("No se pudo editar La contraseña");
                }
                else
                {
                    response.Ok(res, "Contraseña editada.");
                }

            }
            catch (Exception ex)
            {
                response.NotOk(ex.Message);
            }

            return response;
        }

        #region Private methods
        private Rol AsignarRol(Persona persona, int tipoUsuario)
        {
            string query = $"Select * from Roles where Id = ${tipoUsuario}";

            var res = _dbManager.ExecuteQuery(query);

            return res.GetEntity<Rol>();

        }

        public List<Rol> ObtenerAllRoles()
        {
            string query = "select * from Roles";
            var res = _dbManager.ExecuteQuery(query);

            List<Rol> roles = new List<Rol>();

            foreach(DataRow row in res.Rows)
            {
                roles.Add(new Rol
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Nombre = row["Nombre"].ToString(),
                });
            }

            return roles;
        }

        public Usuario ObtenerUsuarioById(int id)
        {
            string query = "select * from Usuarios where id = @id";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };

            var res = _dbManager.ExecuteQuery(query, parameters);

            return res.GetEntity<Usuario>();
        }

        public int ObtenerMedicoPorUsuario(int usuarioId)
        {
            string query = "sp_GetMedicoIdByUsuarioId";
            int IdMedico;
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UsuarioId", usuarioId)
                };

            try
            {
                var res = _dbManager.ExecuteStoredProcedure(query, parameters);

                if(res.Rows.Count == 0)
                {
                    return 0;
                }

                IdMedico =Convert.ToInt32(res.Rows[0]["MedicoId"]);


            }
            catch (Exception ex)
            {
                IdMedico = -1;
            }

            return IdMedico;
        }

        public int ObtenerPacientePorUsuario(int usuarioId)
        {
            string query = "sp_GetPacienteIdByUsuarioId";
            int IdPaciente;
            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UsuarioId", usuarioId)
                };

            try
            {
                var res = _dbManager.ExecuteStoredProcedure(query, parameters);

                if (res.Rows.Count == 0)
                {
                    return 0;
                }

                IdPaciente = Convert.ToInt32(res.Rows[0]["PacienteId"]);


            }
            catch (Exception ex)
            {
                IdPaciente = -1;
            }

            return IdPaciente;
        }




        #endregion
    }
}