using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SolucionIoT.BIZ;
using SolucionIoT.COMMON.Entidades;
using SolucionIoT.COMMON.Modelos;

namespace SolucionIoT.Web.Pages
{
    [Authorize]
    public class PanelUsuario : PageModel
    {
        [BindProperty]
        public PanelUsuarioModel DatosPanelUsuario { get; set; }

        [BindProperty]
        public string idEliminar { get; set; }

        public void OnGet(string idUsuario)
        {
            //DatosPanelUsuario = new PanelUsuarioModel();
            //DatosPanelUsuario.Usuario = FactoryManager.UsuarioManager().BuscarPorId(idUsuario);
            //DatosPanelUsuario.Dispositivos = FactoryManager.DispositivoManager().DispositivosDeUsuarioPorId(idUsuario).ToList();
            DatosPanelUsuario = new PanelUsuarioModel
            {
                Usuario = FactoryManager.UsuarioManager().BuscarPorId(idUsuario),
                Dispositivos = FactoryManager.DispositivoManager().DispositivosDeUsuarioPorId(idUsuario).ToList()
            };
        }

        public void OnPost()
        {
            if (idEliminar != "")
            {
                FactoryManager.DispositivoManager().Eliminar(idEliminar);
            }
        }

    }
}
