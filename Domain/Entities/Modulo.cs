using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Modulo
    { 
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Permiso> Permisos { get; set; }

        public Modulo()
        {
            Permisos = new List<Permiso>();
        }
    }
}
