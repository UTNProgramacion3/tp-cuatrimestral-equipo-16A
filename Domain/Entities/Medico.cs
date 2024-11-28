using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Medico : Empleado
    {
        public int Id { get; set; }
        public int Matricula { get; set; }
        public Especialidad Especialidad { get; set; }
        public int EspecialidadId { get; set; }
    }
}
