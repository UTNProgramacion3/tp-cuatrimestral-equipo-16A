using DataAccess;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Utils;

namespace Business.Managers
{
    public class TurnoManager
    {
        private DBManager _dbManager;
        public Mapper <Turno> _mapper;

        public TurnoManager()
        {
            _dbManager = new DBManager();
            _mapper = new Mapper<Turno>();
        }

        public Turno Crear(Turno turno)
        {
            string query = @"Insert into Turnos values (@IdMedico, @IdPaciente, @Fecha, @Activo)";


            SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@IdMedico", turno.IdMedico),
                    new SqlParameter("@IdPaciente", turno.IdPaciente),
                    new SqlParameter("@Fecha", turno.Fecha),
                    new SqlParameter("@Activo", true)
                   
                };

            try
            {
                var res = _dbManager.ExecuteNonQuery(query, parametros);

                if (res == 0)
                {
                    return new Turno();
                }
                else
                {
                    return turno;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool BajaTurno(int id)
        {
            string query = @"Update Turnos
                            Set Activo = 0
                            Where Id = @Id";

            SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@Id", id)
                };

            try
            {

                var res = _dbManager.ExecuteNonQuery(query, parametros);

                if (res == 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Turno ObtenerPorId(int id)
        {
            string query = @"Select 
                                T.Id,
                                T.IdMedico,
                                T.IdPaciente,
                                T.Fecha,
                                T.Hora,
                                T.IdEstadoTurno,
                                T.EstadoTurno,
                            From Turnos T
                            Inner JOIN Medicos M ON T.IdMedico = T.Id
                            Inner JOIN Pacientes P ON T.IdPaciente = P.Id
                            Inner JOIN EstadoTurno ET ON T.IdEstadoTurno = ET.Id
                            Where T.Id = @Id";

            SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@Id", id)
                };

            try
            {

                DataTable res = _dbManager.ExecuteQuery(query, parametros);

                if (res.Rows.Count == 0)
                {
                    return new Turno();
                }

                Turno turno = _mapper.MapFromRow(res.Rows[0]);

                return turno;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public List <Turno> ObtenerTodos()
        {
            string query = @"SELECT 
                            T.Fecha AS ""Fecha de Turno"",
                            T.Hora AS ""Horario"",
                            M.Nombre AS ""Nombre Medico"",
                            M.Apellido AS ""Apellido Medico"",
                            P.Nombre AS ""Nombre Paciente"",
                            P.Apellido AS ""Apellido Paciente"",
                            ET.Nombre AS ""Estado""
                            FROM Turnos T
                            Inner Join Medicos M ON T.IdMedico = M.Id
                            Inner Join Pacientes P ON T.IdPAciente = P.Id
                            Inner Join EstadoTurno ET ON T.IdEstadoTurno = ET.Id";
                            

            try
            {

                DataTable res = _dbManager.ExecuteQuery(query);

                if (res.Rows.Count == 0)
                {
                    return new List <Turno>();
                }

                var listaTurnos = _mapper.ListMapFromRow(res);

                return listaTurnos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(Turno turno)
        {
            string query = @"Update Turnos 
                            Set IdMedico = @IdMedico,
                                IdPaciente = @IdPaciente,
                                Fecha = @Fecha,
                            Where Id = @Id";

            SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@IdMedico", turno.IdMedico),
                    new SqlParameter("@IdPaciente", turno.IdPaciente),
                    new SqlParameter("@Fecha", turno.Fecha)
                };

            try
            {

                var res = _dbManager.ExecuteNonQuery(query, parametros);

                if (res == 0)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
  
    }
}

