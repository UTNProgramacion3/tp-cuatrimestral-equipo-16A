using Business.Managers;
using Domain.Entities;
using Domain.Response;
using Business.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IPersonaManager : ICrudRepository<Persona>
    {
        Response<PersonaDto> ObtenerPorUsuario(int id);
        bool EditarPersona(string nombre, string apellido, string documento, string telefono, string fechanacimiento, string emailpersonal, int personaId);
    }
}
