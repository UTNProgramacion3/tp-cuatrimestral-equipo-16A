using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Passwordhash { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Rol Rol { get; set; }
        public int RolId { get; set; }
        public string ImagenPerfil { get; set; }
        public bool Activo { get; set; }

        public Usuario() { Rol = new Rol(); }    
    }
}
