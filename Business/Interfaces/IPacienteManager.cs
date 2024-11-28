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
    public interface IPacienteManager : ICrudRepository<Paciente>
    {
        Paciente ObtenerPacienteByUserId(int userId);

        Response<List<PacienteSimpleDto>> ObtenerPacientesFiltrados(string nombre, string apellido, string documento, string obraSocial, string nroAfiliado);

        bool EditarPaciente(string obraSocial, string nroAfiliado, int personaId);
        Response<Paciente> Actualizar(Paciente entity);

        Response<Paciente> ObtenerPorIdTurno(int pacienteId);
    }
}
