using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Turno
    {
        public int Id { get; set; }
        public int IdMedico { get; set; }
        public string NombreMedico { get; set; }
        public string ApellidoMedico { get; set; }
        public int IdPaciente { get; set; }
        public string NombrePaciente { get; set; }
        public string ApellidoPaciente { get; set; }
        public DateTime Fecha { get; set; }
        public string EstadoTurno { get; set; }

        /*Turno()
        {
            Fecha = DateTime.Now;
            Activo = true;
        }*/
    }
}
