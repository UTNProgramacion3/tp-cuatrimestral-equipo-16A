using Business.Dtos;
using Business.Interfaces;
using DataAccess;
using DataAccess.Extensions;
using Domain.Entities;
using Domain.Response;
using System;
using System.Data.SqlClient;

namespace Business.Managers
{
    public class MedicoManager : IMedicoManager
    {
        private readonly DBManager _DBManager;
        private readonly Response<Medico> _response;

        public MedicoManager(DBManager dbManager, Response<Medico> response)
        {
            _DBManager = dbManager;
            _response = response;
        }

        public Response<Medico> CrearMedico(MedicoDto entity)
        {
            string query = "INSERT INTO Medico (Matricula, EspecialidadId, EmpleadoId) VALUES (@Matricula, @EspecialidadId, @EmpleadoId)";
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

    }
}
