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
using Utils;
using Utils.Interfaces;

namespace Business.Managers
{
    public class MedicoManager : IMedicoManager
    {
        private readonly DBManager _DBManager;
        private readonly Response<Medico> _response;
        private readonly Mapper <MedicoDto> _mapper;

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

            if(res == null)
            {
                throw new Exception("Hubo un error al crear el medico");
            }
            _response.Ok(medico);

            return _response;
        }

        public Response<Medico> ObtenerMedicoById(int id)
        {
            string query = @"
                SELECT 
                    m.Id AS MedicoId, m.Matricula, m.EspecialidadId,
                    e.Id AS EmpleadoId, e.Legajo, e.EmailCorporativo, e.Posicion, e.JornadaTrabajoId,
                    p.Id AS PersonaId, p.Nombre, p.Apellido, p.Documento, p.Telefono, 
                    p.FechaNacimiento, p.EmailPersonal, p.DireccionId, p.UsuarioId
                FROM Medico m
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

                Response <List<MedicoDto>> response = new Response<List<MedicoDto>>();  

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

    }
}
