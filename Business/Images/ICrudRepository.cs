using System.Collections.Generic;

namespace Business.Managers
{
    public interface ICrudRepository<T>
    {
        T ObtenerPorId(int id);
        List<T> ObtenerTodos();
        T Crear(T entity);
        bool Update(T entity);
        bool Eliminar(int id);
    }
}