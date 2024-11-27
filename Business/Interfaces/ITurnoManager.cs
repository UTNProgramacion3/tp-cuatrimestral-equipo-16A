using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Dtos;
using System.Data;

namespace Business.Interfaces
{
    public interface ITurnoManager
    {
        TurnoDTO Crear(TurnoDTO dtoTurno);
        List<TurnoDTO> ObtenerTodos();
        DataTable ObtenerTurnosDisponibles(int idMedico, string fecha);
        List<TurnoDTO> ObtenerTurnosPorPacientes(int IdPaciente);
    }
}
