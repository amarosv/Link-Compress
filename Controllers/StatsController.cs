using link_compress_api.BL;
using link_compress_api.ENT;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace link_compress_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        // GET: api/<StatsController>
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<StatsController>/5
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obtiene la información de una estadística dado su ID",
            Description = "Este método recibe un ID y retorna toda la información de la estadística asociada a este."
        )]
        public IActionResult Get(int id)
        {
            IActionResult salida;
            clsStats stats = null;
            try
            {
                stats = clsMetodosStatsBL.getStatsByIdBL(id);
                if (stats == null)
                {
                    salida = NotFound("No se ha encontrado ninguna estadística con ese ID");
                }
                else
                {
                    salida = Ok(stats);
                }
            }
            catch
            {
                salida = BadRequest();
            }

            return salida;
        }

        [HttpGet("alias/{alias}")]
        [SwaggerOperation(
            Summary = "Obtiene todas las estadísticas de un enlace acortado dado su alias",
            Description = "Este método recibe un alias y retorna todas las estadísticas asociadas a este."
        )]
        public IActionResult Get(String alias)
        {
            IActionResult salida;
            List<clsStats> stats = null;
            try
            {
                stats = clsMetodosStatsBL.getAllStatsByAliasBL(alias);
                if (stats == null)
                {
                    salida = NotFound("No se ha encontrado ninguna estadística para ese link compress");
                }
                else
                {
                    salida = Ok(stats);
                }
            }
            catch
            {
                salida = BadRequest();
            }

            return salida;
        }

        // POST api/<StatsController>
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StatsController>/5
        [HttpPut("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StatsController>/5
        [HttpDelete("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Delete(int id)
        {
        }
    }
}
