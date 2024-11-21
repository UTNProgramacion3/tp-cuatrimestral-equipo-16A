using Business.Managers;
using System;
using Domain.Entities;
using Domain.Response;

namespace Business.Interfaces
{
    public interface IUsuarioManager : ICrudRepository<Usuario>
    {
        Response<Usuario> ObtenerPorEmail(string email);   
        bool VerificarPassword(string password, string hashedpassword);
        bool ExisteMail(string email);
        Response<bool> LogIn(Usuario usuario);
        void LogOut(Usuario usuario);
        Usuario GenerarUsuario(Persona persona, int tipoUsuario);
        Response<bool> CambiarPassword(string newPass, int userId);
    }
}
