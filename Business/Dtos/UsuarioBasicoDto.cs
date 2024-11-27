using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos
{
    public class UsuarioBasicoDto : Usuario
    {
        public string NombreCompleto { get; set; }
        public string Rol { get; set; }
    }
}
