using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class HistoriaClinica
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Observaciones { get; set; }
    }
}
