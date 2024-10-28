using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum EmpleadoEnum
    {
        [Description("Medico")]
        Medico = 1,

        [Description("Recepcionista")]
        Recepcionista = 2,

        [Description("Supervisor")]
        Supervisor = 3,

    }
}
