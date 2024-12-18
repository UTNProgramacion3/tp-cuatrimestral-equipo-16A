﻿using Business.Dtos;
using Business.Interfaces;
using DataAccess;
using DataAccess.Extensions;
using Domain.Entities;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Utils;
using Utils.Interfaces;

namespace Business.Managers
{
    public class MedicoManager : IMedicoManager
    {
        private readonly DBManager _DBManager;
        private readonly Response<Medico> _response;
        private readonly Mapper<MedicoDto> _mapper;

        public MedicoManager(DBManager dbManager, Response<Medico> response, Mapper<MedicoDto> mapper)
        {
            _DBManager = dbManager;
            _response = response;
            _mapper = mapper;
        }

        public Response<Medico> CrearMedico(MedicoDto entity)
        {
            string query = "INSERT INTO Medicos (Matricula, EspecialidadId, EmpleadoId) VALUES (@Matricula, @EspecialidadId, @EmpleadoId)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Matricula", entity.Matricula),
                new SqlParameter("@EspecialidadId", entity.EspecialidadId),
                new SqlParameter("@EmpleadoId", entity.EmpleadoId)
            };

            var res = _DBManager.ExecuteQuery(query, parameters);

            Medico medico = new Medico
            {
                Matricula = entity.Matricula,
                EspecialidadId = entity.EspecialidadId,
            };

            medico.Id = res.GetId();

            if (res == null)
            {
                throw new Exception("Hubo un error al crear el medico");
            }
            _response.Ok(medico);

            return _response;
        }

        public bool ActualizarEspecialidad(int matricula, int nuevaEspecialidadId)
        {
            string query = "UPDATE Medicos SET EspecialidadId = @EspecialidadId WHERE Matricula = @Matricula";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EspecialidadId", nuevaEspecialidadId),
                new SqlParameter("@Matricula", matricula)
            };


            try
            {
               var res = _DBManager.ExecuteNonQuery(query, parameters);

                return res > 0;
            }catch (Exception ex)
            {
                return false;
            }
        }

        public Response<Medico> ActualizarMedico(MedicoDto entity)
        {
            try
            {
                string query = @"
                UPDATE Medicos
                SET Matricula = @Matricula,
                EspecialidadId = @EspecialidadId,
                WHERE EmpleadoId = @EmpleadoId;
                ";

                string retrieveData = "SELECT * FROM Medicos WHERE Id = @Id";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Matricula", entity.Matricula),
                    new SqlParameter("@EspecialidadId", entity.EspecialidadId),
                    new SqlParameter("@EmpleadoId", entity.EmpleadoId),
                };

                var res = _DBManager.ExecuteNonQueryAndGetData(query, parameters, retrieveData);

                if (res == null)
                {
                    throw new Exception("Error al actualizar el médico.");
                }

                var medicoActualizado = res.GetEntity<Medico>();
                _response.Ok(medicoActualizado);

                return _response;
            }
            catch (Exception ex)
            {
                _response.NotOk($"Error al actualizar el médico: {ex.Message}");
                return _response;
            }
        }


        public Response<Medico> ObtenerMedicoById(int id)
        {
            string query = @"
                            SELECT 
                                m.Id AS Id, m.Matricula, m.EspecialidadId,
                                e.Id AS EmpleadoId, e.Legajo, e.EmailCorporativo, e.JornadaTrabajoId,
                                p.Id AS PersonaId, p.Nombre, p.Apellido, p.Documento, p.Telefono, 
                                p.FechaNacimiento, p.EmailPersonal, p.DireccionId, p.UsuarioId
                            FROM Medicos m
                            INNER JOIN Empleados e ON m.EmpleadoId = e.Id
                            INNER JOIN Personas p ON e.PersonaId = p.Id
                            WHERE m.Id = @Id";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            };

            var res = _DBManager.ExecuteQuery(query, parameters);

            if (res == null)
            {
                throw new Exception("No se encontró el medico con el ID proporcionado");
            }

            var medico = res.GetEntity<Medico>();

            _response.Ok(medico);

            return _response;
        }

        public Response<List<MedicoDto>> ObtenerTodos()
        {
            string query = @"
                Select
                M.Id AS Medico_Id,
	            PE.Apellido AS Persona_Apellido,
	            PE.Nombre AS Persona_Nombre,
	            ES.Nombre AS Especialidad_Nombre
                From Medicos M
                Inner Join Especialidades ES ON M.EspecialidadId = ES.Id
                Inner Join Empleados EM ON M.EmpleadoId = EM.Id
                Inner Join Personas PE ON EM.PersonaId = PE.Id";

            DataTable res = new DataTable();

            try
            {
                res = _DBManager.ExecuteQuery(query);
                int rows = res.Rows.Count;

                if (rows == 0)
                {
                    return new Response<List<MedicoDto>>();
                }

                Response<List<MedicoDto>> response = new Response<List<MedicoDto>>();

                response.Data = _mapper.ListMapFromRow(res);

                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Especialidad> ObtenerTodasEspecialidades()
        {
            string query = "select * from Especialidades";

            var res = _DBManager.ExecuteQuery(query);

            List<Especialidad> especialidades = new List<Especialidad>();

            foreach (DataRow row in res.Rows)
            {
                especialidades.Add(new Especialidad
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Nombre = row["Nombre"].ToString()
                });
            }

            return especialidades;
        }

        public Medico ObtenerMedicoByUserId(int userId)
        {
            string query = @"SELECT  
            EMP.*,
            P.*,
            MED.*,
            DIR.*
        FROM 
            Medicos MED
        LEFT JOIN 
            Empleados EMP ON MED.EmpleadoId = EMP.Id
        LEFT JOIN 
            Personas P ON P.Id = EMP.PersonaId
        LEFT JOIN 
            JornadasTrabajo JT ON JT.Id = EMP.JornadaTrabajoId
        LEFT JOIN 
            DiasLaborales DL ON DL.JornadaTrabajoId = JT.Id
        LEFT JOIN 
            Direcciones DIR ON DIR.Id = P.DireccionId
        WHERE 
            P.UsuarioId = @UserId;
            ";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserId", userId)
            };

            var res = _DBManager.ExecuteQuery(query, parameters);

            if (res.Rows.Count == 0)
            {
                return null;
            }
            var medico = res.GetEntity<Medico>();
            var direccion = new Direccion
            {
                Id = Convert.ToInt32(res.Rows[0]["Id"]),
                Calle = res.Rows[0]["Calle"].ToString(),
                Numero = (int)res.Rows[0]["Numero"],
                Localidad = res.Rows[0]["Localidad"].ToString(),
                Provincia = res.Rows[0]["Provincia"].ToString(),
                CodigoPostal = res.Rows[0]["CodigoPostal"].ToString()
            };
            medico.Direccion = direccion;
            return medico;
        }
        public Response<List<MedicoDto>> ObtenerTodosBySede(int IdSede)
        {
            string query = @"
                Select
                M.Id AS Medico_Id,
                PE.Apellido AS Persona_Apellido,
                PE.Nombre AS Persona_Nombre,
                ES.Nombre AS Especialidad_Nombre
                From Medicos M
                Inner Join Especialidades ES ON M.EspecialidadId = ES.Id
                Inner Join Empleados EM ON M.EmpleadoId = EM.Id
                Inner Join Personas PE ON EM.PersonaId = PE.Id
                Inner Join JornadasTrabajo JT ON EM.JornadaTrabajoId = EM.JornadaTrabajoId
                Where JT.SedeId = @IdSede";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@IdSede", IdSede)
            };

            DataTable res = new DataTable();

            try
            {
                res = _DBManager.ExecuteQuery(query, parameters);
                int rows = res.Rows.Count;

                if (rows == 0)
                {
                    return new Response<List<MedicoDto>>();
                }

                Response<List<MedicoDto>> response = new Response<List<MedicoDto>>();

                response.Data = _mapper.ListMapFromRow(res);

                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Response<List<MedicoDto>> ObtenerTodosConFiltro(string nombre, string matricula, int especialidadId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            string query = @"
                            Select
                            M.Id AS Medico_Id,
                            M.Matricula,
                            PE.Apellido AS Persona_Apellido,
                            PE.Nombre AS Persona_Nombre,
                            ES.Nombre AS Especialidad_Nombre,
                            ES.Id AS EspecialidadId
                            From Medicos M
                            Inner Join Especialidades ES ON M.EspecialidadId = ES.Id
                            Inner Join Empleados EM ON M.EmpleadoId = EM.Id
                            Inner Join Personas PE ON EM.PersonaId = PE.Id
                            WHERE 1=1";

  
            if (!string.IsNullOrEmpty(nombre))
            {
                query += " AND (PE.Nombre LIKE @Nombre OR PE.Apellido LIKE @Nombre)";
                parameters.Add(new SqlParameter("@Nombre", "%" + nombre + "%"));
            }

            if (!string.IsNullOrEmpty(matricula))
            {
                query += " AND M.Matricula LIKE @Matricula";
                parameters.Add(new SqlParameter("@Matricula", "%" + matricula + "%"));
            }

            if (especialidadId > 0)
            {
                query += " AND M.EspecialidadId = @EspecialidadId";
                parameters.Add(new SqlParameter("@EspecialidadId", especialidadId));
            }
            DataTable res = _DBManager.ExecuteQuery(query, parameters.ToArray());

            Response<List<MedicoDto>> response = new Response<List<MedicoDto>>();
            response.Data = _mapper.ListMapFromRow(res);

            return response;
        }

        List<MedicoSimpleDto> IMedicoManager.ObtenerTodosConFiltro(string nombre, string apellido, string matricula, int? especialidadId)
        {
            string query = @"
                            SELECT
                                M.Matricula,
                                M.EspecialidadId,
                                ES.Nombre AS EspecialidadNombre,
                                M.EmpleadoId,
                                PE.Nombre AS PersonaNombre,
                                PE.Apellido AS PersonaApellido
                            FROM Medicos M
                            INNER JOIN Especialidades ES ON M.EspecialidadId = ES.Id
                            INNER JOIN Empleados EM ON M.EmpleadoId = EM.Id
                            INNER JOIN Personas PE ON EM.PersonaId = PE.Id
                            WHERE 1=1";

            if (!string.IsNullOrEmpty(nombre))
            {
                query += " AND PE.Nombre LIKE @Nombre";
            }

            if (!string.IsNullOrEmpty(apellido))
            {
                query += " AND PE.Apellido LIKE @Apellido";
            }

            if (!string.IsNullOrEmpty(matricula))
            {
                query += " AND M.Matricula LIKE @Matricula";
            }

            if (especialidadId > 0)
            {
                query += " AND M.EspecialidadId = @EspecialidadId";
            }

            var parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(nombre))
            {
                parameters.Add(new SqlParameter("@Nombre", "%" + nombre + "%"));
            }

            if (!string.IsNullOrEmpty(apellido))
            {
                parameters.Add(new SqlParameter("@Apellido", "%" + apellido + "%"));
            }

            if (!string.IsNullOrEmpty(matricula))
            {
                parameters.Add(new SqlParameter("@Matricula", "%" + matricula + "%"));
            }

            if (especialidadId > 0)
            {
                parameters.Add(new SqlParameter("@EspecialidadId", especialidadId));
            }
            var result = _DBManager.ExecuteQuery(query, parameters.ToArray());

            List<MedicoSimpleDto> medicos = new List<MedicoSimpleDto>();

            foreach (DataRow row in result.Rows)
            {
                medicos.Add(new MedicoSimpleDto
                {
                    Matricula = Convert.ToInt32(row["Matricula"]),
                    EspecialidadId = Convert.ToInt32(row["EspecialidadId"]),
                    EspecialidadNombre = row["EspecialidadNombre"].ToString(),
                    EmpleadoId = Convert.ToInt32(row["EmpleadoId"]),
                    PersonaNombre = row["PersonaNombre"].ToString(),
                    PersonaApellido = row["PersonaApellido"].ToString()
                });
            }

            return medicos;
        }

      
    }
}
