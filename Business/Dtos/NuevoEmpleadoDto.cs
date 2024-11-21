using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos
{
    public class NuevoEmpleadoDto : Persona
    {
        public int Legajo { get; set; }
        public string EmailCorporativo { get; set; }
        public int Posicion { get; set; }
        public JornadaTrabajo HorarioTrabajo { get; set; }
        public int JornadaTrabajoId { get; set; }
        public int RolId { get; set; }
        public int Matricula { get; set; }
        public int EspecialidadId { get; set; }
    }
}
