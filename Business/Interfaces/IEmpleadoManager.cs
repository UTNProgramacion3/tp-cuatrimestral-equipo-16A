using Business.Dtos;
using Business.Managers;
using Domain.Entities;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IEmpleadoManager : ICrudRepository<Empleado>
    {
        Response<Empleado> CrearNuevo(NuevoEmpleadoDto entity, List<DiaLaboral> jornada, int sede);
    }
}
