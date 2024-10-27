using Business.Managers;
using System;
using Domain.Entities;
using Domain.Response;

namespace Business.Interfaces
{
    public interface IUsuarioManager : ICrudRepository<Usuario>
    {
        Response<Usuario> ObtenerPorEmail(string email);   
        Response<bool> VerificarPassword(string password, string hashedpassword);
        Response<bool> LogIn(Usuario usuario);
        Response<bool> LogOut(Usuario usuario);
    }
}
