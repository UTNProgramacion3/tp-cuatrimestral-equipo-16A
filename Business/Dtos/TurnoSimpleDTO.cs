using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos
{
    public class TurnoSimpleDTO
    {
        public int Id { get; set; }
        public string MedicoNombre { get; set; }
        public string PacienteNombre { get; set; }
        public string SedeNombre { get; set; }
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Estado { get; set; }
    }
}
