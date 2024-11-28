using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos
{
    public class MedicoSimpleDto
    {
        public int Matricula { get; set; }
        public int EspecialidadId { get; set; }
        public string EspecialidadNombre { get; set; }  
        public int EmpleadoId { get; set; }
        public string PersonaNombre { get; set; }  
        public string PersonaApellido { get; set; }  
    }


}
