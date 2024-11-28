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
    public interface IMedicoManager
    {
        Response<Medico> CrearMedico(MedicoDto entity);
        Response<Medico> ObtenerMedicoById(int id);
        Response<List<MedicoDto>> ObtenerTodos();
        List<Especialidad> ObtenerTodasEspecialidades();
        Medico ObtenerMedicoByUserId(int userId);
        bool ActualizarEspecialidad(int matricula, int nuevaEspecialidadId);
        List<MedicoSimpleDto> ObtenerTodosConFiltro(string nombre, string apellido, string matricula, int? especialidadId);
        Response<Medico> ActualizarMedico(MedicoDto entity);
    }
}
