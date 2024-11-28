using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Dtos;
using System.Data;
using Domain.Response;

namespace Business.Interfaces
{
    public interface ITurnoManager
    {
        TurnoDTO Crear(TurnoDTO dtoTurno);
        List<TurnoDTO> ObtenerTodos();
        DataTable ObtenerTurnosDisponibles(int idMedico, string fecha);

        Response<List<TurnoSimpleDTO>> ObtenerTurnosPorMedico(int idMedico);
        Response<List<TurnoSimpleDTO>> ObtenerTurnosPorPaciente(int idPaciente);

        Response<bool> GuardarComentario(int turnoId, string comentario);
        string ObtenerComentario(int turnoId);
        List<TurnoDTO> ObtenerTurnosPorPacientes(int IdPaciente);
    }
}
