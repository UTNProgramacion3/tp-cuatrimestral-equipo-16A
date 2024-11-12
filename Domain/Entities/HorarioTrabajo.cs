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
       public List<Jornada> Jornada { get; set; }
    }
}
