using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos
{
    public class EmailValidationDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime TiempoExpiracion { get; set; }
    }
}
