using Business.Interfaces;
using DataAccess;
using DataAccess.Extensions;
using Domain.Entities;
using Domain.Enums;
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
               new SqlParameter("@SedeId", entity.Sede.Id)
            };


            var res = _DBManager.ExecuteQuery(query, parameters);
            entity.Id = res.GetId();

            RegistrarJornadaSemanal(entity.Jornada, entity.Id);

            _jornadaTrabajo.Ok(entity);

            return _jornadaTrabajo;
        }

        public DateTime DisponibilidadCambioJornada(int empleadoId)
        {

            string query = "Select * from Empleados where Id = @Id";
            var res = _DBManager.ExecuteQuery(query, new SqlParameter[] { new SqlParameter("@Id", empleadoId) });

            Empleado empleado = res.GetEntity<Empleado>();

            if(empleado.Posicion == (int)PosicionEnum.Medico)
            {
                // Necesitamos traernos la última fecha donde el empleado tiene un turno, para poder cambiar la jornada a partir de esa fecha (Y no afectar los turnos ya asignados).
            }

            //Implementar cambio de disponibilidad.
            return DateTime.Now;

            throw new NotImplementedException();
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

        public JornadaTrabajo ObtenerJornadaEmpleado(int empleadoId)
        {
            string query = "Select * from Empleados where Id = @Id";
            var res = _DBManager.ExecuteQuery(query, new SqlParameter[] { new SqlParameter("@Id", empleadoId) });

            Empleado empleado = res.GetEntity<Empleado>();

            query = "Select * from JornadaTrabajo where Id = @JornadaTrabajoId";

            res = _DBManager.ExecuteQuery(query, new SqlParameter[] { new SqlParameter("@JornadaTrabajoId", empleado.JornadaTrabajoId) });

            return res.GetEntity<JornadaTrabajo>();
        }
    }
}
