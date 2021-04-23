using SolucionIoT.COMMON.Entidades;
using SolucionIoT.COMMON.Interfaces;
using SolucionIoT.COMMON.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SolucionIoT.BIZ.API
{
    public class LecturaManager : GenericManager<Lectura>, ILecturaManager
    {
        private async Task <IEnumerable<Lectura>> LecturasDelDispositivoAsync(string id)
        {
            ConsultaAPIModel model = new ConsultaAPIModel()
            {
                NombreMetodo = "LecturasDelDispositivo",
                Parametros = new List<string> { id }
            };
            return await TraerDatos(model).ConfigureAwait(false);
        }
        
        private async Task <IEnumerable<Lectura>> LecturasDelDispositivoAsync(string id, DateTime inicio, DateTime fin)
        {
            ConsultaAPIModel model = new ConsultaAPIModel()
            {
                NombreMetodo = "LecturasDelDispositivo",
                Parametros = new List<string> { id, inicio.ToString(),fin.ToString()}
            };
            return await TraerDatos(model).ConfigureAwait(false);
        }




        public IEnumerable<Lectura> LecturasDelDispositivo(string id)
        {
            return LecturasDelDispositivoAsync(id).Result;
        }

        public IEnumerable<Lectura> LecturasDelDispositivo(string id, DateTime inicio, DateTime fin)
        {
            return LecturasDelDispositivoAsync(id,inicio,fin).Result;
           
        }
    }
}
