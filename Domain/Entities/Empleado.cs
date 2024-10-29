using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Empleado : Persona
    {
        public int Id { get; set; }
        public int Legajo { get; set; }
        public string EmailCorporativo { get; set; }
        public int Posicion { get; set; }
        public JornadaTrabajo HorarioTrabajo { get; set; }
    }
}
