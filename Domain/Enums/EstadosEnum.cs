using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum EstadosEnum
    {
        [Description("Confirmado")]
        Confirmado = 1,

        [Description("Cancelado")]
        Cancelado = 2,

        [Description("Reprogramado")]
        Reprogramado = 3,

        [Description("Finalizado")]
        Finalizado = 4,

        [Description("Ausente")]
        Ausente = 5,

    }
}
