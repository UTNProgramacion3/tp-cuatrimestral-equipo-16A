﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos
{
    public class EmpleadoDto : Empleado
    {
        public string Password { get; set; }
    }
}
