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
    public class LecturaController : GenericApiController<Lectura>
    {
        static ILecturaManager manager = FactoryManager.LecturaManager();

        public LecturaController() : base(manager)
        {
        }

        public override ActionResult<IEnumerable<Lectura>> Consulta([FromBody] ConsultaAPIModel model, string id)
        {
            List<Lectura> datos = null;
            try
            {
                if (model.NombreMetodo == "LecturasDelDispositivo")
                {
                    switch (model.Parametros.Count)
                    {
                        case 1:

                            datos = manager.LecturasDelDispositivo(model.Parametros[0]).ToList(); 
                            break;
                        case 3:

                            datos = manager.LecturasDelDispositivo(model.Parametros[0], DateTime.Parse(model.Parametros[1]),DateTime.Parse(model.Parametros[2])).ToList(); 
                            break;

                        default:
                            datos = null;
                            break;
                    }
                    if (datos == null)
                    {
                        return BadRequest("NUmero de parametros incorrectos");
                    }
                    else
                    {
                        return Ok(datos);
                    }

                }
                else
                {
                    return BadRequest("Nombre no encontrado");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
