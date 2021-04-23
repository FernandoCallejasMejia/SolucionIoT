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
    public class DispositivoController : GenericApiController<Dispositivo>
    {
        static IDispositivoManager manager = FactoryManager.DispositivoManager();

        public DispositivoController() : base(manager)
        {
        }

        public override ActionResult<IEnumerable<Dispositivo>> Consulta([FromBody] ConsultaAPIModel model, string id)
        {
            List<Dispositivo> datos = null;
            try
            {
                switch (model.NombreMetodo)
                {
                    case "DispositivoPerteneceAUsuario":
                        datos = new List<Dispositivo>();
                        datos.Add (manager.DispositivoPerteneceAUsuario(model.Parametros[0], model.Parametros[1]));
                        break;
                    case "DispositivosDeUsuarioPorEmail" :
                        datos = manager.DispositivosDeUsuarioPorEmail(model.Parametros[0]).ToList();
                        break;
                    case "DispositivosDeUsuarioPorId":
                        datos = manager.DispositivosDeUsuarioPorId(model.Parametros[0]).ToList();
                        break;
                    default:
                        datos = null;
                        break;
                }
                if (datos == null)
                {
                    return BadRequest("Nombre no encontrado");
                }
                else
                {
                    return Ok(datos);
                }
            }
            catch (Exception)
            {

                return BadRequest("Error en los datos");
            }
        }
    }
}
