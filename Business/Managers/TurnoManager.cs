using DataAccess;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Utils.Interfaces;
using Utils;
using Business;
using Business.Dtos;
using Business.Interfaces;

namespace Business.Managers
{
    public class TurnoManager : ITurnoManager
    {
        private DBManager _dbManager;
        public IMapper <TurnoDTO> _mapper;

        public TurnoManager()
        {
            _dbManager = new DBManager();
            _mapper = new Mapper<TurnoDTO>();
        }

        public TurnoDTO Crear(TurnoDTO dtoTurno)
        {
            string query = @"Insert into Turnos (IdMedico, IdPaciente, IdEstadoTurno, IdSede, Fecha, Hora, Observaciones) VALUES (@IdMedico, @IdPaciente, @IdEstadoTurno, @IdSede, @Fecha, @Hora, @Observaciones)";


            SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@IdMedico", dtoTurno.Medico.Id),
                    new SqlParameter("@IdPaciente", dtoTurno.Paciente.Id),
                    new SqlParameter("@IdEstadoTurno", dtoTurno.EstadoTurno.Id),
                    new SqlParameter("@IdSede", dtoTurno.Sede.Id),
                    new SqlParameter("@Fecha", dtoTurno.Turno.Fecha),
                    new SqlParameter("@Hora", dtoTurno.Turno.Hora),
                    new SqlParameter("@Observaciones", dtoTurno.Turno.Observaciones)
                   
                };

            try
            {
                var res = _dbManager.ExecuteNonQuery(query, parametros);

                if (res == 0)
                {
                    return new TurnoDTO();
                }
                else
                {
                    return dtoTurno;
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
        /*public TurnoDTO ObtenerPorId(int id)
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
                    return new TurnoDTO();
                }

                TurnoDTO dtoTurno = _mapper.MapFromRow(res.Rows[0]);

                return dtoTurno;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }*/

        public List <TurnoDTO> ObtenerTodos()
        {
            string query = @"Select	T.Fecha AS Turno_Fecha,
		                    T.Hora AS Turno_Hora,
		                    dbo.fn_buscar_nombre(EM.PersonaId) AS Medico_Nombre,
		                    dbo.fn_buscar_apellido(EM.PersonaId) AS Medico_Apellido,
		                    dbo.fn_buscar_nombre(PA.PersonaId) AS Paciente_Nombre,
		                    dbo.fn_buscar_apellido(PA.PersonaId) AS Paciente_Apellido,
		                    SE.Nombre AS Sede_Nombre,
		                    ET.Estado AS Turno_EstadoTurno
                            From Turnos T
                            Left Join Empleados EM ON T.IdMedico = EM.Id
                            Left Join Pacientes PA ON T.IdPaciente = PA.Id
                            Left Join EstadoTurnos ET ON T.IdEstadoTurno = ET.Id
                            Left Join Sedes SE ON T.IdSede = SE.Id";
                            

            try
            {

                DataTable res = _dbManager.ExecuteQuery(query);

                if (res.Rows.Count == 0)
                {
                    return new List <TurnoDTO>();
                }

                var listaTurnos = _mapper.ListMapFromRow(res);

                return listaTurnos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerTurnosDisponibles(int idMedico, string fecha)
        {

            //string query = @"EXEC ObtenerTurnosDisponibles @MedicoId = @idMedico, @Fecha = @fecha;";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter ("@MedicoId", idMedico),
                new SqlParameter ("@Fecha", fecha)
            };

            try
            {
                DataTable res = _dbManager.ExecuteStoredProcedure("ObtenerTurnosDisponibles", parameters);

                if (res.Rows.Count == 0)
                {
                    return new DataTable();
                }

                return res;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        /*public bool Update(Turno turno)
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

        }*/

    }
}

