using SolucionIoT.COMMON.Entidades;
using SolucionIoT.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SolucionIoT.COMMON.Modelos;

namespace SolucionIoT.BIZ.API
{
    public abstract class GenericManager<T> : IGenericManager<T> where T:BaseDTO
    {
        internal HttpClient client;
        internal readonly string uriApi;
        internal string error;
        public GenericManager()
        {
            client = new HttpClient();
            client.BaseAddress =new Uri( "http://localhost:4430/");
            uriApi = "api/" + typeof(T).Name;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async Task<IEnumerable<T>> ObtenerDatos()
        {
            HttpResponseMessage response = await client.GetAsync(uriApi).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var items = JsonConvert.DeserializeObject<IEnumerable<T>>(content);
                return items;
            }
            else
            {
                error = "No se pudieron obtener los tados ";
                return null;
            }
        }

        private async Task<T> Insert(T entidad)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(entidad), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uriApi, content).ConfigureAwait(false);
           
            if (response.IsSuccessStatusCode)
            {
                var r = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var item = JsonConvert.DeserializeObject<T>(r);
                return item;
            }
            else
            {
                error = "No se pudo insertar el dato";
                return null;
            }
        }

        private async Task<T> SearchById(string id)
        {
            HttpResponseMessage response = await client.GetAsync(uriApi+"/"+id).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var items = JsonConvert.DeserializeObject<T>(content);
                return items;
            }
            else
            {
                error = "No se pudieron obtener los tados ";
                return null;
            }
        }

        private async Task<T> Update(T entidad)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(entidad), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(uriApi, content).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var r = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var item = JsonConvert.DeserializeObject<T>(r);
                return item;
            }
            else
            {
                error = "No se pudo actualizar el dato";
                return null;
            }
        }

        private async Task<bool> Delete(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(uriApi + "/" + id).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var item= JsonConvert.DeserializeObject<bool>(content);
                return item;
            }
            else
            {
                error = "No se pudo eliminar el dato ";
                return false;
            }
        }


        internal async Task<IEnumerable<T>> TraerDatos(ConsultaAPIModel model)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uriApi + "/consulta", content).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var r = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var item = JsonConvert.DeserializeObject<IEnumerable<T>>(r);
                return item;
            }
            else
            {
                error = "No se pudo actualizar el dato";
                return null;
            }
        }



        public string Error => error;

        public IEnumerable<T> ObtenerTodos => ObtenerDatos().Result;

        public T Actualizar(T entidad)
        {
            return Update(entidad).Result;
        }

        public T BuscarPorId(string id)
        {
            return SearchById(id).Result;
        }

        public bool Eliminar(string id)
        {
            return Delete(id).Result;
        }

        public T Insertar(T entidad)
        {
            return Insert(entidad).Result;
        }
    }
    
}
