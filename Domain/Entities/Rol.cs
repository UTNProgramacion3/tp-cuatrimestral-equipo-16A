using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Permiso> Permisos { get; set; }
        public bool Activo { get; set; }

        public Rol() { Permisos = new List<Permiso>(); }    
    }
}
