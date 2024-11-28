using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Permiso
    {
        public int Id { get; set; }
        public string Nombre{ get; set; }
        public Modulo Modulo { get; set; }

        public Permiso() { Modulo = new Modulo(); }
    }
}
