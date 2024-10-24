using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class HorarioTrabajo
    {
        public int Id { get; set; } 
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public List<DiasEnum> DiasLaborales { get; set; }
    }
}
