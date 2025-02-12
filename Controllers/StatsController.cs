using link_compress_api.BL;
using link_compress_api.ENT;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace link_compress_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        // GET: api/<StatsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<StatsController>/5
        [HttpGet("{id}")]
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

        // POST api/<StatsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StatsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StatsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
