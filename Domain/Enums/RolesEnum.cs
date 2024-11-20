using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum RolesEnum
    {
        [Description("Administrador")]
        Administrador = 1,

        [Description("Recepcionista")]
        Recepcionista = 2,

        [Description("Medico")]
        Medico = 3,

        [Description("Paciente")]
        Paciente = 4,

        [Description("Empleado")]
        Empleado = 5,
    }
}
