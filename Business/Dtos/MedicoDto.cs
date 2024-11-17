using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos
{
    public class MedicoDto
    {
        public int Matricula { get; set; }
        public int EspecialidadId { get; set; }
        public int EmpleadoId { get; set; }
        public Medico Medico { get; set; }
        public Especialidad Especialidad { get; set; }
        public Persona Persona { get; set; }
    }
}
