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
    public class AccionController : GenericApiController<Accion>
    {
        static IAccionManager manager = FactoryManager.AccionManager();
        public AccionController() : base(manager)
        {
        }

        public override ActionResult<IEnumerable<Accion>> Consulta([FromBody] ConsultaAPIModel model, string id)
        {
            try
            {
                List<Accion> datos;
                switch (model.NombreMetodo)
                {
                    case "AccionesDelDispositivo":
                        switch (model.Parametros.Count)
                        {
                            case 1:
                                datos = manager.AccionesDelDispositivo(model.Parametros[0]).ToList();
                                break;
                            case 3:
                                datos = manager.AccionesDelDispositivo(model.Parametros[0].ToString(), DateTime.Parse(model.Parametros[1]), DateTime.Parse(model.Parametros[2])).ToList();
                                break;
                            case 4:
                                datos = manager.AccionesDelDispositivo(model.Parametros[0], model.Parametros[1], DateTime.Parse(model.Parametros[2]),
                                    DateTime.Parse( model.Parametros[3])).ToList();
                                break;

                            default:
                                datos = null;
                                break;
                        }
                        break;

                    default:

                        datos = null;
                        break;

                }
                if (datos == null)
                {
                    return BadRequest("Nombre del metodo no encotrado");
                }
                else
                {
                    return Ok(datos);

                }

            }
            catch (Exception )
            {

                    return BadRequest("Error");
                throw;
            }
             
            
           

        }
    }
}
