using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Log
    {
        public int Id { get; set; } 
        public Usuario Usuario { get; set; }
        public string Accion { get; set; }
        public DateTime Fecha { get; set; }
        public string Detalles { get; set; }
    }
}
