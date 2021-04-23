using SolucionIoT.COMMON.Entidades;
using SolucionIoT.COMMON.Interfaces;
using SolucionIoT.COMMON.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolucionIoT.BIZ.API
{
    public class UsuarioManager : GenericManager<Usuario>, IUsuarioManager
    {
        private async Task <Usuario> LoginAsync(string email, string password)
        {
            ConsultaAPIModel model = new ConsultaAPIModel()
            {
                NombreMetodo = "Login",
                Parametros = new List<string> { email, password }
            };
            var r = await TraerDatos(model).ConfigureAwait(false);
            var result = r.ToList();
            return result[0];
        }

        public Usuario Login(string email, string password)
        {
            return LoginAsync(email, password).Result;
        }
    }
}
