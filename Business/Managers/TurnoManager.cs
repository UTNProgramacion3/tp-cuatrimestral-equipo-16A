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
using Domain.Response;

namespace Business.Managers
{
    public class TurnoManager : ITurnoManager
    {
        private DBManager _dbManager;
        public IMapper<TurnoDTO> _mapper;

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
        
        public List<TurnoDTO> ObtenerTurnosPorPacientes(int IdPaciente)
        {         
            string query = @"SELECT   
                                T.Id,
                                T.IdMedico,
                                T.IdPaciente,
                                T.Fecha,
                                T.Hora,
                                T.IdEstadoTurno,
                                ET.Estado,
                                T.Fecha,
                                T.Hora,
                                T.Observaciones,
                                T.IdEstadoTurno
                            From Turnos T
                            LEFT JOIN Medicos M ON T.IdMedico = T.Id
                            LEFT JOIN Pacientes P ON T.IdPaciente = P.Id
                            LEFT JOIN EstadoTurnos ET ON T.IdEstadoTurno = ET.Id
                            Where T.IdPaciente = @IdPaciente";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@IdPaciente", IdPaciente)
            };

            try
            {
                DataTable res = _dbManager.ExecuteQuery(query, parameters);
                
                if(res.Rows.Count==0)
                {
                    return new List<TurnoDTO>();
                }


                var listaTurnos = _mapper.ListMapFromRow(res);

                return listaTurnos;


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        
        public TurnoDTO ObtenerPorId(int id)
        {
            string query = @"Select 
                                T.Id AS Turno_Id,
                                T.IdMedico AS Medico_Id,
                                T.IdPaciente AS Paciente_Id,
                                SE.Id AS Sede_Id,
                                T.Fecha AS Turno_Fecha,
                                T.Hora AS Turno_Hora,
                                T.IdEstadoTurno AS EstadoTurno_Id,
                                ET.Estado AS EstadoTurno_Nombre,
                                T.Observaciones AS Turno_Observaciones
                            FROM Turnos T
                            LEFT JOIN Medicos M ON T.IdMedico = M.Id
                            LEFT JOIN Pacientes P ON T.IdPaciente = P.Id
                            LEFT JOIN EstadoTurnos ET ON T.IdEstadoTurno = ET.Id
                            LEFT JOIN Sedes SE ON T.IdSede = SE.id
                            Where T.Id = @id";

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
        }



        public List<TurnoDTO> ObtenerTodos()
        {
            string query = @"Select
                            T.Id AS Turno_Id,
                            T.Fecha AS Turno_Fecha,
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
                    return new List<TurnoDTO>();
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

        public Response<List<TurnoSimpleDTO>> ObtenerTurnosPorPaciente(int idPaciente)
        {
            string query = @"SELECT 
                             T.Id AS Id,
                             dbo.fn_buscar_nombre(EM.PersonaId) + ' ' + dbo.fn_buscar_apellido(EM.PersonaId) AS MedicoNombre,
                             dbo.fn_buscar_nombre(PA.PersonaId) + ' ' + dbo.fn_buscar_apellido(PA.PersonaId) AS PacienteNombre,
                             SE.Nombre AS SedeNombre,
                                CONVERT(VARCHAR, T.Fecha, 103) AS Fecha,  -- Formato dd/mm/yyyy
                                CONVERT(VARCHAR, T.Hora, 8) AS Hora,      -- Formato HH:mm:ss
                                ET.Estado AS Estado
                            FROM 
                                Turnos T
                            LEFT JOIN 
                                Empleados EM ON T.IdMedico = EM.Id
                            LEFT JOIN 
                                Pacientes PA ON T.IdPaciente = PA.Id
                            LEFT JOIN 
                                EstadoTurnos ET ON T.IdEstadoTurno = ET.Id
                            LEFT JOIN 
                                Sedes SE ON T.IdSede = SE.Id
                            WHERE 
                                T.IdPaciente = @IdPaciente";

            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter ("@IdPaciente", idPaciente)
                };

            Response<List<TurnoSimpleDTO>> response = new Response<List<TurnoSimpleDTO>>();
            Mapper<TurnoSimpleDTO> map = new Mapper<TurnoSimpleDTO>();

            try
            {
                var res = _dbManager.ExecuteQuery(query, parameters);

                if (res.Rows.Count == 0)
                {
                    response.NotOk("Error al buscar los turnos del paciente.");
                    return response;
                }

                var turnos = map.ListMapFromRow(res);

                if (turnos.Count == 0)
                {
                    response.NotOk("Error al mapear los turnos.");
                    return response;
                }

                response.Ok(turnos);
            }
            catch (Exception ex)
            {
                response.NotOk(ex.Message);
            }

            return response;
        }

        Response<List<TurnoSimpleDTO>> ITurnoManager.ObtenerTurnosPorMedico(int idMedico)
        {
            string query = @"SELECT 
                             T.Id AS Id,
                             dbo.fn_buscar_nombre(EM.PersonaId) + ' ' + dbo.fn_buscar_apellido(EM.PersonaId) AS MedicoNombre,
                             dbo.fn_buscar_nombre(PA.PersonaId) + ' ' + dbo.fn_buscar_apellido(PA.PersonaId) AS PacienteNombre,
                             SE.Nombre AS SedeNombre,
                                CONVERT(VARCHAR, T.Fecha, 103) AS Fecha,  -- Formato dd/mm/yyyy
                                CONVERT(VARCHAR, T.Hora, 8) AS Hora,      -- Formato HH:mm:ss
                                ET.Estado AS Estado
                            FROM 
                                Turnos T
                            LEFT JOIN 
                                Empleados EM ON T.IdMedico = EM.Id
                            LEFT JOIN 
                                Pacientes PA ON T.IdPaciente = PA.Id
                            LEFT JOIN 
                                EstadoTurnos ET ON T.IdEstadoTurno = ET.Id
                            LEFT JOIN 
                                Sedes SE ON T.IdSede = SE.Id
                            WHERE 
                                T.IdMedico = @IdMedico";

            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter ("@IdMedico", idMedico)
                };

            Response<List<TurnoSimpleDTO>> response = new Response<List<TurnoSimpleDTO>>();
            Mapper<TurnoSimpleDTO> map = new Mapper<TurnoSimpleDTO> ();

            try
            {
                var res = _dbManager.ExecuteQuery(query, parameters);

                if(res.Rows.Count == 0)
                {
                    response.NotOk("Error al buscar los turnos del medico.");
                    return response;
                }

                var turnos = map.ListMapFromRow(res);

                if (turnos.Count == 0)
                {
                    response.NotOk("Error al mapear los turnos.");
                    return response;
                }

                response.Ok(turnos);
            }
            catch (Exception ex)
            {
                response.NotOk (ex.Message);
            }

            return response;
        }

        public bool CancelarOReprogramarTurno(int IdEstado, int IdTurno)
        {
            string query = @"Update Turnos 
                            Set
                            IdEstadoTurno = @IdEstado,
                            Activo = 0
                            Where Id = @IdTurno";

            SqlParameter[] parametros = new SqlParameter[]
                {
                    new SqlParameter("@IdEstado",IdEstado),
                    new SqlParameter("@IdTurno", IdTurno)
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

        public Response<bool> GuardarComentario(int turnoId, string comentario)
        {
            string query = @"UPDATE Turnos
                            SET Observaciones = @observaciones
                            WHERE Id = @idturno";

            SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter ("@observaciones", comentario),
                    new SqlParameter ("@idturno", turnoId)
                };

            var response = new Response<bool> ();

            try
            {
                var res = _dbManager.ExecuteNonQuery(query, parameters);

                if(res == 0)
                {
                    response.NotOk("No se pudo agregar el comentario.");
                    return response;
                }

                response.Ok(Convert.ToBoolean(res));

            }catch (Exception ex)
            {
                response.NotOk(ex.Message);

            }

            return response;
        }

        public string ObtenerComentario(int turnoId)
        {
            string query = "SELECT Observaciones FROM Turnos WHERE Id = @turnoId";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter ("@turnoId", turnoId)
            };
            string comentario;

            try
            {
                var res = _dbManager.ExecuteQuery(query, parameters);

                if(res.Rows.Count == 0)
                {
                    return string.Empty;
                }

                comentario = res.Rows[0]["Observaciones"].ToString();

            }catch (Exception ex)
            {
                comentario = ex.Message;
            }

            return comentario;

        }
    }
}

