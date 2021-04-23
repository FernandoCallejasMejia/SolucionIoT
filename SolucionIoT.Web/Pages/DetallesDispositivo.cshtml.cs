using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SolucionIoT.BIZ;
using SolucionIoT.COMMON.Entidades;
using SolucionIoT.Tools;

namespace SolucionIoT.Web.Pages
{
    [Authorize]
    public class DetallesDispositivoModel : PageModel
    {
        [BindProperty]
        public Dictionary<string,string> Opciones { get; set; }
        [BindProperty]
        public Dispositivo Dispositivo { get; set; }
        [BindProperty]
        public List<Lectura> Lecturas { get; set; }
        public void OnGet(string id,string opcion)
        {
            
            Dispositivo = FactoryManager.DispositivoManager().BuscarPorId(id);

#if DEBUG
            Random r = new Random();

                FactoryManager.LecturaManager().Insertar(new Lectura()
                {
                    IdDispositivo = id,
                    Temperatura = r.Next(2, 40),
                    Humedad = r.Next(0, 100),
                    Luminosidad = r.Next(0, 1024)
                });
#endif
            Lecturas = FactoryManager.LecturaManager().LecturasDelDispositivo(id).ToList();

            if (opcion != null)
            {
                MqttService.Publicar("SolucionIoT/" + id, opcion);
            }

            Opciones = new Dictionary<string, string>
            {
                { "R11", $"Encender {Dispositivo.UsoRelevador1}" },
                { "R10", $"Apagar {Dispositivo.UsoRelevador1}" },
                { "R21", $"Encender {Dispositivo.UsoRelevador2}" },
                { "R20", $"Apagar {Dispositivo.UsoRelevador2}" },
                { "R31", $"Encender {Dispositivo.UsoRelevador3}" },
                { "R30", $"Apagar {Dispositivo.UsoRelevador3}" },
                { "R41", $"Encender {Dispositivo.UsoRelevador4}" },
                { "R40", $"Apagar {Dispositivo.UsoRelevador4}" },
                { "B1", $"Encender {Dispositivo.UsoBuzzer}" },
                { "B0", $"Apagar {Dispositivo.UsoBuzzer}" }
            };
        }
    }
}
