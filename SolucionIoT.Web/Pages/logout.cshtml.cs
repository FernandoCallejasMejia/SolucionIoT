using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SolucionIoT.Web.Pages
{
    [Authorize]
    public class logoutModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.SignOutAsync();
        }
    }
}
