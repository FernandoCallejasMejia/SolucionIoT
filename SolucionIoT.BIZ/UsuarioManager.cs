using SolucionIoT.COMMON.Entidades;
using SolucionIoT.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolucionIoT.BIZ
{
    public class UsuarioManager : GenericManager<Usuario>, IUsuarioManager
    {
        public UsuarioManager(IGenericRepository<Usuario> repository) : base(repository)
        {
        }


        public Usuario Login(string email, string password)
        {
            return repository.Query(u => u.Correo == email && u.Password == password).ToList().SingleOrDefault();
        }
    }
}
