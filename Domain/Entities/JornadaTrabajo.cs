using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class JornadaTrabajo
    {
        public int Id { get; set; } 
        public Sede Sede { get; set; }
        public List<DiaLaboral> Jornada { get; set; }
    }
}
