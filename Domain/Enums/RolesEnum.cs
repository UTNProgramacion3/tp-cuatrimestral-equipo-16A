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
        [Description("Admin")]
        Administrador = 1,

        [Description("Medico")]
        Medico = 2,

        [Description("Recepcionista")]
        Recepcionista = 3,

        [Description("Empleado")]
        Empleado = 4,

        [Description("Paciente")]
        Paciente = 5,
    }
}
