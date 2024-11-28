using Business.Dtos;
using Business.Managers;
using DataAccess;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IEmailManager
    {
        Task EnviarEmail(string destinatario, string subject, string body);
        Task EnviarMailValidacionNuevaCuenta(string destinatario, int usuarioId, string nombreUsuario);
        Task EnviarEstadoTurno(TurnoDTO turno, Paciente paciente);
    }
}
