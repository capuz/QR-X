using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Isp.Laboratorios.Infrastructure
{

    public interface IRepository<T> where T : class
    {
        void Insertar(T entity);
        void Actualizar(T entity);
        void Eliminar(T entity);
        void EliminarPorId(int id);
        List<T> BuscarPor(Expression<Func<T, bool>> predicate);
        List<T> ObtenerTodo();
        T ObtenerPorId(int id);
    }
}
