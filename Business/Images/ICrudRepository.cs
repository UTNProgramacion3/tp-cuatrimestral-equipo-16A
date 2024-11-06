using Domain.Response;
using System.Collections.Generic;

namespace Business.Managers
{
    public interface ICrudRepository<T> where T : class, new()
    {
        Response<T> ObtenerPorId(int id);
        Response<List<T>> ObtenerTodos();
        Response<T> Crear(T entity);
        Response<bool> Update(T entity);
        Response<bool> Eliminar(int id);
    }
}