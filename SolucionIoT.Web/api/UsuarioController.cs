using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolucionIoT.BIZ;
using SolucionIoT.COMMON.Entidades;
using SolucionIoT.COMMON.Interfaces;
using SolucionIoT.COMMON.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolucionIoT.Web.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : GenericApiController<Usuario>
    {
        static IUsuarioManager manager = FactoryManager.UsuarioManager();
        public UsuarioController() : base(manager)
        {
        }

        public override ActionResult<IEnumerable<Usuario>> Consulta([FromBody] ConsultaAPIModel model, string id)
        {
            try
            {
                List<Usuario> datos=null;
                if (model.NombreMetodo == "Login")
                {
                    datos = new List<Usuario>();
                    datos.Add  (manager.Login(model.Parametros[0], model.Parametros[1]));
                    return Ok(datos);
                }
                else
                {
                    return BadRequest("Nombre del metodo no encontrado");
                }

            }
            catch (Exception)
            {

                return BadRequest("Error al procesar los datos");
            }      
        }
    }
}
