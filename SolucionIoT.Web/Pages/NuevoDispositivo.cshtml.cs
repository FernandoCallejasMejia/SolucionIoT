using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SolucionIoT.BIZ;
using SolucionIoT.COMMON.Entidades;

namespace SolucionIoT.Web.Pages
{
    [Authorize]
    public class NuevoDispositivoModel : PageModel
    {
        [BindProperty]
        public Dispositivo Dispositivo { get; set; }
        [BindProperty]

        public string Mensaje { get; set; }
        public void OnGet(string id)
        {
            Dispositivo = new Dispositivo();
            Dispositivo.IdUsuario = id;
            Mensaje = "";
        }

        public async Task<ActionResult> OnPost()
        {
   
            if (FactoryManager.DispositivoManager().Insertar(Dispositivo) != null)
            {
                return RedirectToPage("/PanelUsuario", new { idUsuario = Dispositivo.IdUsuario });

            }
            else
            {
                Mensaje = "Error al crear el dispositivo";
                return Page();
            }
        }
    }
}
