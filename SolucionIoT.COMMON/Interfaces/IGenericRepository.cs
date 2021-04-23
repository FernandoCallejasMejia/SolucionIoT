using SolucionIoT.COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SolucionIoT.COMMON.Interfaces
{
    public interface IGenericRepository<T> where T:BaseDTO
    {
        string Error { get;  }
        IEnumerable<T> Read { get; }

        T Create(T entidad);

        T Update(T entidad);

        bool Delete(string id);

        T SearchById(string id);

        IEnumerable<T> Query(Expression<Func<T, bool>> predicado);
    }
}
