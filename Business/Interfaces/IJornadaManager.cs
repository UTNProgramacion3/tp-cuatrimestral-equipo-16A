using Business.Managers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IJornadaManager : ICrudRepository<JornadaTrabajo>
    {
        DateTime DisponibilidadCambioJornada(int empleadoId);
        JornadaTrabajo ObtenerJornadaEmpleado(int empleadoId);
    }
}
