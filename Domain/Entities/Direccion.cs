using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Direccion
    {
        public int Id { get; set; }
        public string Calle {  get; set; }
        public int Numero { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
    }
}
