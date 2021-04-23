using Newtonsoft.Json;
using SolucionIoT.COMMON.Entidades;
using SolucionIoT.COMMON.Interfaces;
using SolucionIoT.COMMON.Modelos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SolucionIoT.BIZ.API
{
    public class AccionManager : GenericManager<Accion>, IAccionManager
    {
        private async Task<IEnumerable<Accion>> AccionesDelDispositivoAsync(string id)
        {
            ConsultaAPIModel model = new ConsultaAPIModel()
            {
                NombreMetodo = "AccionesDelDispositivo",
                Parametros = new List<string>() { id }
            };
            return await TraerDatos(model).ConfigureAwait(false);
        }

        private async Task<IEnumerable<Accion>> TraerDatos(ConsultaAPIModel model)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uriApi + "/consulta", content).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var r = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var item = JsonConvert.DeserializeObject<IEnumerable<Accion>>(r);
                return item;
            }
            else
            {
                error = "No se pudo actualizar el dato";
                return null;
            }
        }

        private async Task<IEnumerable<Accion>> AccionesDelDispositivoAsync(string id, DateTime inicio, DateTime fin)
        {
            ConsultaAPIModel model = new ConsultaAPIModel()
            {
                NombreMetodo = "AccionesDelDispositivo",
                Parametros = new List<string>() { id,inicio.ToString(),fin.ToString() }
            };
            return await TraerDatos(model).ConfigureAwait(false);

        }

        private async Task<IEnumerable<Accion>> AccionesDelDispositivoAsync(string id, string actuador, DateTime inicio, DateTime fin)
        {
            ConsultaAPIModel model = new ConsultaAPIModel()
            {
                NombreMetodo = "AccionesDelDispositivo",
                Parametros = new List<string>() { id, inicio.ToString(),inicio.ToString(), fin.ToString() }
            };
            return await TraerDatos(model).ConfigureAwait(false);

        }



        public IEnumerable<Accion> AccionesDelDispositivo(string id)
        {
            return AccionesDelDispositivoAsync(id).Result;
        }

        public IEnumerable<Accion> AccionesDelDispositivo(string id, DateTime inicio, DateTime fin)
        {
            return AccionesDelDispositivoAsync(id, inicio, fin).Result;
        }

        public IEnumerable<Accion> AccionesDelDispositivo(string id, string actuador, DateTime inicio, DateTime fin)
        {
            return AccionesDelDispositivoAsync(id, actuador, inicio, fin).Result;
        }
    }
}
