using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;

namespace Business.Dtos
{
    public class TurnoDTO
    {
        public Turno Turno { get; set; }
        public Medico Medico { get; set; }
        public Paciente Paciente { get; set; }
        public Sede Sede { get; set; }
        public EstadoTurno EstadoTurno { get; set; }
        
        public TurnoDTO()
        {
            Turno = new Turno();
            Medico = new Medico();
            Paciente = new Paciente();
            Sede = new Sede();
            EstadoTurno = new EstadoTurno();
        }
    }

}
