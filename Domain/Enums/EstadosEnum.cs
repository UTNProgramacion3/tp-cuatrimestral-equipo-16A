﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum EstadosEnum
    {
        [Description("Pendiente")]
        Pendiente = 1,

        [Description("Confirmado")]
        Confirmado = 2,

        [Description("Cancelado")]
        Cancelado = 3,

    }
}
