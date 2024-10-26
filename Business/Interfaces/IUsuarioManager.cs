using Business.Managers;
using System;
using Domain.Entities;

namespace Business.Interfaces
{
    public interface IUsuarioManager : ICrudRepository<Usuario>
    {
        Usuario ObtenerPorEmail(string email);   
        bool VerificarPassword(string password);
        Usuario LogIn(Usuario usuario);
    }
}
