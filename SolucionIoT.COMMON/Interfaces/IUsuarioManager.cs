using SolucionIoT.COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SolucionIoT.COMMON.Interfaces
{
   public interface IUsuarioManager : IGenericManager <Usuario> 
    {

        Usuario Login(string email, string password);
    }
}
