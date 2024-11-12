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

        [Description("Empleado")]
        Empleado = 2,

        [Description("Paciente")]
        Paciente = 3,
    }
}
