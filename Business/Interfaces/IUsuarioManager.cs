using Business.Managers;
using System;
using Domain.Entities;
using Domain.Response;
using System.Collections.Generic;
using Business.Dtos;

namespace Business.Interfaces
{
    public interface IUsuarioManager : ICrudRepository<Usuario>
    {
        Response<Usuario> ObtenerPorEmail(string email);   
        bool VerificarPassword(string password, string hashedpassword);
        bool ExisteMail(string email);
        Response<Usuario> LogIn(Usuario usuario);
        void LogOut(Usuario usuario);
        Usuario GenerarUsuario(Persona persona, int tipoUsuario);
        Usuario ValidarToken(string token);
        Usuario ActivarUsuario(Usuario usuario, string password);
        Response<bool> CambiarPassword(string newPass, int userId);
        List<Rol> ObtenerAllRoles();
        Usuario ObtenerUsuarioById(int id);
        List<UsuarioBasicoDto> ObtenerUsuariosDataBasica();
    }
}
