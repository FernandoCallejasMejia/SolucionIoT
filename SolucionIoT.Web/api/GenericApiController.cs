using Microsoft.AspNetCore.Mvc;
using SolucionIoT.COMMON.Entidades;
using SolucionIoT.COMMON.Interfaces;
using SolucionIoT.COMMON.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SolucionIoT.Web.api
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class  GenericApiController<T> : ControllerBase where T:BaseDTO
    {
         IGenericManager<T> manager;

        public GenericApiController(IGenericManager<T> genericManager)
        {
            manager = genericManager;
        }
        // GET: api/<GenericApiController>
        [HttpGet]
        public ActionResult<IEnumerable<T>> Get()
        {
            try
            {
                return Ok(manager.ObtenerTodos);
            }
            catch (Exception)
            {
                return BadRequest(manager.Error);
                throw;
            }
        }

        // GET api/<GenericApiController>/5
        [HttpGet("{id}")]
        public ActionResult<T> Get(string id)
        {
            try
            {
                return Ok(manager.BuscarPorId(id));
            }
            catch (Exception)
            {

                return BadRequest(manager.Error);
            }
        }

        // POST api/<GenericApiController>
        [HttpPost]
        public ActionResult<T> Post([FromBody] T value)
        {
            try
            {
                return Ok(manager.Insertar(value));
            }
            catch (Exception)
            {

                return BadRequest(manager.Error);
            }
        }

        // PUT api/<GenericApiController>/5
       // [HttpPut("{id}")]
        [HttpPut]
        public ActionResult<T> Put( [FromBody] T value)
        {
            try
            {
                return Ok(manager.Actualizar(value));
            }
            catch (Exception)
            {

                return BadRequest(manager.Error);
            }
        }

        // DELETE api/<GenericApiController>/5
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(string id)
        {
            try
            {
                return Ok(manager.Eliminar(id));
            }
            catch (Exception)
            {
                return BadRequest(manager.Error);
                throw;
            }
        }

        [HttpPost("{id}")]
        public abstract ActionResult<IEnumerable<T>> Consulta([FromBody] ConsultaAPIModel model, string id);
    }
}
