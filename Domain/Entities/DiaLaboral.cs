using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DiaLaboral
    {
        public int Id { get; set; }
        public int Dia { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public int JornadaTrabajoId { get; set;}
    }
}
