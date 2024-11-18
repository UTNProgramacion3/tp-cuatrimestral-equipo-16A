using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos
{
    public class SedeDto
    {
        public Sede Sede { get; set; }
        public Direccion Direccion { get; set; }

    }
}
