using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SolucionIoT.BIZ;
using SolucionIoT.COMMON.Entidades;
using SolucionIoT.COMMON.Interfaces;
using SolucionIoT.COMMON.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SolucionIoT.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        [BindProperty]
        public LoginModel Login { get; set; }
        [BindProperty]
        public bool EsLogin { get; set; }
        [BindProperty]
        public string Error { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {


        }
        public async Task<IActionResult> OnPost()
        {
            if (EsLogin)
            {
                IUsuarioManager usuarioManager = FactoryManager.UsuarioManager();
                Usuario u = usuarioManager.Login(Login.Correo, Login.Password);
                if (u != null)
                {
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,u.Nombre),
                        new Claim(ClaimTypes.Email,u.Correo)
                    };
                    ClaimsIdentity identity = new ClaimsIdentity(claims, "cookie");
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(scheme: "Seguridad",principal:principal, properties: new AuthenticationProperties{
                        ExpiresUtc=DateTime.UtcNow.AddMinutes(10)
                    });
                    return RedirectToPage("PanelUsuario", new { idUsuario = u.Id });
                }
                else
                {
                    Error = "Usuario y/o  contraseña incorrecto";
                    return Page();
                }
            }
            else
            {
                return RedirectToPage("NuevoUsuario");
            }

        }
    }
}
