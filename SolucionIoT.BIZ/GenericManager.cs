using SolucionIoT.COMMON.Entidades;
using SolucionIoT.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolucionIoT.BIZ
{
    public abstract class GenericManager<T> : IGenericManager<T> where T : BaseDTO
    {
        internal IGenericRepository<T> repository;

        public GenericManager(IGenericRepository<T> repository)
        {
            this.repository = repository;
        }
        public IEnumerable<T> ObtenerTodos => repository.Read;


        public string Error => repository.Error;

        public T Actualizar(T entidad)
        {
            return repository.Update(entidad);
        
        }

        public bool Eliminar(string id)
        {
            return repository.Delete(id);
        }

        public T BuscarPorId(string id)
        {
            return repository.SearchById(id);
        }

  
        public T Insertar(T entidad)
        {
            return repository.Create(entidad);
        }
    }
}
