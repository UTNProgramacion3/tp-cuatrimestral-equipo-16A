using Business.Interfaces;
using DataAccess;
using Domain.Entities;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class JornadaManager : IJornadaManager
    {
        private readonly DBManager _DBManager;
        private readonly Response<JornadaTrabajo> _jornadaTrabajo;

        public JornadaManager(DBManager dBManager, Response<JornadaTrabajo> jornadaTrabajo)
        {
            _DBManager = dBManager;
            _jornadaTrabajo = jornadaTrabajo;
        }
        public Response<JornadaTrabajo> Crear(JornadaTrabajo entity)
        {
           var query = "Insert into JornadaTrabajo values(@SedeId)";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@HoraInicio", entity.HoraInicio),
                new SqlParameter("@HoraFin", entity.HoraFin),
                new SqlParameter("@Fecha", entity.Fecha),
                new SqlParameter("@EmpleadoId", entity.Empleado.Id),
                new SqlParameter("@TipoJornadaId", entity.TipoJornada.Id),
                new SqlParameter("@Observaciones", entity.Observaciones),
            };

            var res = _DBManager.ExecuteQuery(query, parameters);

            _jornadaTrabajo.Ok(entity);
            return _jornadaTrabajo;
        }

        public Response<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public Response<JornadaTrabajo> ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Response<List<JornadaTrabajo>> ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        public Response<bool> Update(JornadaTrabajo entity)
        {
            throw new NotImplementedException();
        }

        public void RegistrarJornadaSemanal(List<DiaLaboral> jornadas,int jornadaId)
        {
            var query = "Insert into DiaLaboral values(@Dia, @Inicio, @Fin, @JornadaTrabajoId)";
            foreach (var dia in jornadas)
            {
                _DBManager.ExecuteQuery(query, new SqlParameter[]
                {
                    new SqlParameter("@Dia", dia.Dia),
                    new SqlParameter("@Inicio", dia.Inicio),
                    new SqlParameter("@Fin", dia.Fin),
                    new SqlParameter("@JornadaTrabajoId", jornadaId)
                });
            }
        }
    }
}
