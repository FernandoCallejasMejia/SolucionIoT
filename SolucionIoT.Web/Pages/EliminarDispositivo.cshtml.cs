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
    public class EliminarDispositivoModel : PageModel
    {
        [BindProperty]
        public Dispositivo Dispositivo { get; set; }
        [BindProperty]
        public string Mensaje { get; set; }
        public void OnGet(string id)
        {
            Dispositivo = FactoryManager.DispositivoManager().BuscarPorId(id);
            Mensaje = "";
        }
        public async Task<ActionResult> OnPost()
        {
            string id = Dispositivo.IdUsuario;
            if (FactoryManager.DispositivoManager().Eliminar(Dispositivo.Id))
            {
                return RedirectToPage("/PanelUsuario", new { idUsuario = id});
            }
            else
            {
                Mensaje = "Error al eliminar el dispositivo";
                return Page();
            }
        }
    }
}
