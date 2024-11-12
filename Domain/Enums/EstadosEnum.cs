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
        [Description("Nuevo")]
        Nuevo = 1,

        [Description("Ausente")]
        Vencido = 2,

        [Description("Cancelado")]
        Cancelado = 3,

        [Description("Reprogramado")]
        Reprogramado = 4,

        [Description("Finalizado")]
        Finalizado = 5
    }
}
