using KalumManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using KalumManagement.Dtos;
using KalumManagement.Utilities;

namespace KalumManagement.Controllers
{
    [ApiController]
    [Route("kalum-management/v1/jornadas")]
    public class JornadaController : ControllerBase
    {
        private readonly KalumDBContext DbContext;
        private readonly ILogger<JornadaController> Logger;

        public JornadaController(KalumDBContext _dBContext, ILogger<JornadaController> _logger)
        {
            this.DbContext = _dBContext;
            this.Logger = _logger;
        }


        [HttpGet("page/{page}")]
        public async Task<ActionResult<IEnumerable<Jornada>>> GetJornadaPagination(int page)
        {
            var queryable = this.DbContext.Jornada.AsQueryable();
            int registros = await queryable.CountAsync();
            if (registros == 0)
            {
                return NoContent();
            }
            else
            {
                var jornadas = await queryable.OrderBy(Jornada => Jornada.NombreCorto).Pagination(page).ToListAsync();
                PaginationResponse<Jornada> response = new PaginationResponse<Jornada>(jornadas, page, registros);
                return Ok(response);

            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jornada>>> Get()
        {
            Logger.LogDebug("iniciando proceso de consulta");
            List<Jornada> jornada = await DbContext.Jornada.ToListAsync();
            Logger.LogDebug("finalizando el proceso de consulta");
            if (jornada == null || jornada.Count == 0)
            {
                Logger.LogWarning("No existen registros en la base de datos");
                return new NoContentResult();

            }

            Logger.LogInformation("Se ejecuto de forma exitosa la consulta de la informacion");
            return Ok(jornada);

        }

        [HttpGet("{id}", Name = "GetJornada")]

        public async Task<ActionResult<Jornada>> GetJornada(String id)
        {
            Logger.LogDebug($"iniciando proceso de consulta con id {id}");
            var jornada = await DbContext.Jornada.FirstOrDefaultAsync(ct => ct.JornadaId == id);
            if (jornada == null)
            {
                Logger.LogWarning($"No existen registros en la base de datos{id}");
                return new NoContentResult();
            }

            Logger.LogInformation("Se ejecuto de forma exitosa la consulta de la informacion");
            return Ok(jornada);

        }

        [HttpPost]
        public async Task<ActionResult<Jornada>> Post([FromBody] Jornada value)
        {
            Logger.LogDebug($"Iniciando proceso de registro con la siguiente informacion {value}");
            value.JornadaId = Guid.NewGuid().ToString().ToUpper();
            Logger.LogDebug($"Generación de llave con valor {value.JornadaId}");
            await DbContext.Jornada.AddAsync(value);
            await this.DbContext.SaveChangesAsync();
            Logger.LogInformation($"Se ejecuto exitosamente el proceso de almacenamiento");
            return new CreatedAtRouteResult("GetJornada", new { id = value.JornadaId }, value);

        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<Jornada>> Delete(string id)
        {
            Logger.LogDebug($"Iniciando proceso de registro con la siguiente informacion {id}");
            Jornada jornada = await this.DbContext.Jornada.FirstOrDefaultAsync(ct => ct.JornadaId == id);
            if (jornada == null)
            {
                Logger.LogWarning($"No existen registros en la base de datos con el id {id}");
                return NotFound();
            }

            else
            {
                this.DbContext.Jornada.Remove(jornada);
                await this.DbContext.SaveChangesAsync();
                Logger.LogInformation("Se ejecuto de forma exitosa la eliminación del registro");
                return jornada;
            }

        }

        [HttpPut("{id}")]

        public async Task<ActionResult> Put(string id, [FromBody] Jornada value)
        {
            Logger.LogDebug($"Iniciando proceso de actualizacion de información");
            Jornada jornada = await this.DbContext.Jornada.FirstOrDefaultAsync(ct => ct.JornadaId == id);
            if (jornada == null)
            {
                Logger.LogWarning($"No existen registros en la base de datos con el id {id}");

                return NotFound();
            }
            jornada.NombreCorto = value.NombreCorto;
            this.DbContext.Entry(jornada).State = EntityState.Modified;
            await this.DbContext.SaveChangesAsync();
            Logger.LogInformation("Se ejecuto de forma exitosa la actualización del registro");

            return NoContent();
        }



    }
}