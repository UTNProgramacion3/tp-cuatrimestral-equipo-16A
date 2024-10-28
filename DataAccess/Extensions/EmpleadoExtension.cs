using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Extensions
{
    public static class EmpleadoExtension
    {
        public static string CrearEmailCorporativo(this Empleado empleado)
        {
            return empleado.Nombre + "." + empleado.Apellido + "@clinica.com";
        }
    }
}
