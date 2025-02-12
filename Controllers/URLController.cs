using link_compress_api.BL;
using link_compress_api.ENT;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;  // Asegúrate de tener esta directiva de using para Swagger

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace link_compress_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class URLController : ControllerBase
    {
        // GET: api/<URLController>
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]  // Esto ocultará la acción en Swagger
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<URLController>/5
        [HttpGet("id/{id}")]
        [SwaggerOperation(
            Summary = "Obtiene los datos de un enlace acortado asociado a un ID",
            Description = "Este método recibe un ID y retorna los datos del enlace acortado correspondiente. " +
                "Si no se encuentran los datos, se devuelve un resultado vacío."
        )]
        public IActionResult Get(int id)
        {
            IActionResult salida;
            clsURL url = null;
            try
            {
                url = clsMetodosURLBL.findURLByIdBL(id);
                if (url == null)
                {
                    salida = NotFound("No se ha encontrado ningún enlace acortado con ese ID");
                }
                else
                {
                    salida = Ok(url);
                }
            }
            catch
            {
                salida = BadRequest();
            }

            return salida;
        }

        [HttpGet("{alias}")]
        [SwaggerOperation(
            Summary = "Obtiene los datos de un enlace acortado asociado a un alias",
            Description = "Este método recibe un alias y retorna los datos del enlace acortado correspondiente. " +
                "Si no se encuentran los datos, se devuelve un resultado vacío."
        )]
        public IActionResult Get(String alias)
        {
            IActionResult salida;
            clsURL url = null;
            try
            {
                url = clsMetodosURLBL.findURLByAliasBL(alias);
                if (url == null)
                {
                    salida = NotFound("No se ha encontrado ningún enlace acortado con ese alias");
                }
                else
                {
                    salida = Ok(url);
                }
            }
            catch
            {
                salida = BadRequest();
            }

            return salida;
        }

        // POST api/<URLController>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Crea un link compress con un alias aleatorio",
            Description = "Este método recibe una url y crea un link compress con alias aleatorio que apunta hacia ella"
        )]
        public IActionResult Post(String url)
        {
            IActionResult salida;
            String alias = "";

            try
            {
                alias = clsMetodosURLBL.createLinkCompressBL(url);
                if (string.IsNullOrEmpty(alias))
                {
                    salida = NotFound("Ha ocurrido un error");
                }
                else
                {
                    salida = Ok(alias);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest();
            }

            return salida;
        }
        
        [HttpPost("alias")]
        [SwaggerOperation(
            Summary = "Crea un link compress con un alias personalizado",
            Description = "Este método recibe una url y un alias y crea un link compress con alias personalizado que apunta hacia ella"
        )]
        public IActionResult Post(String url, String alias)
        {
            IActionResult salida;
            int numeroFilasAfectadas = 0;

            try
            {
                numeroFilasAfectadas = clsMetodosURLBL.createPersonalizatedLinkCompressBL(url, alias);
                if (numeroFilasAfectadas == 0)
                {
                    salida = NotFound("Ha ocurrido un error. Prueba con otro alias");
                }
                else
                {
                    salida = Ok(alias);
                }
            }
            catch (Exception e)
            {
                salida = BadRequest();
            }

            return salida;
        }

        // PUT api/<URLController>/5
        [HttpPut("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<URLController>/5
        [HttpDelete("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Delete(int id)
        {
        }
    }
}
