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
    public class DispositivoManager : GenericManager<Dispositivo>, IDispositivoManager
    {
        private async Task <Dispositivo> DispositivoPerteneceAUsuarioAsync(string idDispositivo, string idUsuario)
        {
            ConsultaAPIModel model = new ConsultaAPIModel()
            {
                NombreMetodo = "DispositivoPerteneceAUsuario",
                Parametros = new List<string> { idDispositivo, idUsuario }
            };
            var r= await TraerDatos(model).ConfigureAwait(false);
            var result=r.ToList();
            return result[0];



        }

        private async Task<IEnumerable<Dispositivo>> DispositivosDeUsuarioPorEmailAsync(string email)
        {
            ConsultaAPIModel model = new ConsultaAPIModel()
            {
                NombreMetodo = "DispositivosDeUsuarioPorEmail",
                Parametros = new List<string> { email }
            };
            return await TraerDatos(model).ConfigureAwait(false);

        }

        private async Task<IEnumerable<Dispositivo>> DispositivosDeUsuarioPorIdAsync(string id)
        {
            ConsultaAPIModel model = new ConsultaAPIModel()
            {
                NombreMetodo = "DispositivosDeUsuarioPorId",
                Parametros = new List<string> { id }
            };
            return await TraerDatos(model).ConfigureAwait(false);

        }


        public Dispositivo DispositivoPerteneceAUsuario(string idDispositivo, string idUsuario)
        {
            return DispositivoPerteneceAUsuarioAsync(idDispositivo, idUsuario).Result;
        }

        public IEnumerable<Dispositivo> DispositivosDeUsuarioPorEmail(string email)
        {
            return DispositivosDeUsuarioPorEmailAsync(email).Result;
        }

        public IEnumerable<Dispositivo> DispositivosDeUsuarioPorId(string id)
        {
            return DispositivosDeUsuarioPorIdAsync(id).Result;
        }
    }
}
