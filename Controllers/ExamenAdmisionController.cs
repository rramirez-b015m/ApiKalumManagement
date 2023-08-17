using KalumManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using KalumManagement.Dtos;
using KalumManagement.Utilities;

namespace KalumManagement.Controllers
{

    [ApiController]
    [Route("kalum-management/v1/examen-admision")]
    public class ExamenAdmisionController : ControllerBase
    {
         private readonly KalumDBContext DbContext;
        private readonly ILogger<ExamenAdmisionController> Logger;

        public ExamenAdmisionController(KalumDBContext _dBContext, ILogger<ExamenAdmisionController> _logger)
        {
            this.DbContext = _dBContext;
            this.Logger = _logger;
        }

        [HttpGet("page/{page}")]
        public async Task<ActionResult<IEnumerable<ExamenAdmision>>> GetExamenAdmisionPagination(int page)
        {
            var queryable = this.DbContext.ExamenAdmision.AsQueryable();
            int registros = await queryable.CountAsync();
            if (registros == 0)
            {
                return NoContent();
            }
            else
            {
                var examenadmision = await queryable.OrderBy(ExamenAdmision => ExamenAdmision.ExamenId).Pagination(page).ToListAsync();
                PaginationResponse<ExamenAdmision> response = new PaginationResponse<ExamenAdmision>(examenadmision, page, registros);
                return Ok(response);

            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamenAdmisionListDTO>>> Get()
        {
            Logger.LogDebug("iniciando proceso de consulta");
            List<ExamenAdmision> examenadmision = await DbContext.ExamenAdmision.ToListAsync();
            Logger.LogDebug("finalizando el proceso de consulta");
            if (examenadmision == null || examenadmision.Count == 0)
            {
                Logger.LogWarning("No existen registros en la base de datos");
                return new NoContentResult();
                
            }

            Logger.LogInformation("Se ejecuto de forma exitosa la consulta de la informacion");
            return Ok(examenadmision);
            
        }

         [HttpGet("{id}", Name = "GetExamenAdmision")]

        public async Task<ActionResult<ExamenAdmision>> GetExamen(String id)
        {
            Logger.LogDebug($"iniciando proceso de consulta con id {id}");
            var examenadmision = await DbContext.ExamenAdmision.FirstOrDefaultAsync(ct => ct.ExamenId == id);
            if (examenadmision == null)
            {
                Logger.LogWarning($"No existen registros en la base de datos{id}");
                return new NoContentResult();
            }

            Logger.LogInformation("Se ejecuto de forma exitosa la consulta de la informacion");
            return Ok(examenadmision);

        }

        [HttpPost]
        public async Task<ActionResult<ExamenAdmision>> Post([FromBody] ExamenAdmision value)
        {
            Logger.LogDebug($"Iniciando proceso de registro con la siguiente informacion {value}");
            value.ExamenId = Guid.NewGuid().ToString().ToUpper();
            Logger.LogDebug($"Generación de llave con valor {value.ExamenId}");
            await DbContext.ExamenAdmision.AddAsync(value);
            await this.DbContext.SaveChangesAsync();
            Logger.LogInformation($"Se ejecuto exitosamente el proceso de almacenamiento");
            return new CreatedAtRouteResult("GetExamenAdmision", new { id = value.ExamenId }, value);

        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<ExamenAdmision>> Delete(string id)
        {
            Logger.LogDebug($"Iniciando proceso de registro con la siguiente informacion {id}");
            ExamenAdmision examenadmision = await this.DbContext.ExamenAdmision.FirstOrDefaultAsync(ct => ct.ExamenId == id);
            if (examenadmision == null)
            {
                Logger.LogWarning($"No existen registros en la base de datos con el id {id}");
                return NotFound();
            }

            else
            {
                this.DbContext.ExamenAdmision.Remove(examenadmision);
                await this.DbContext.SaveChangesAsync();
                Logger.LogInformation("Se ejecuto de forma exitosa la eliminación del registro");
                return examenadmision;
            }

        }

        [HttpPut("{id}")]

        public async Task<ActionResult> Put(string id, [FromBody] ExamenAdmision value)
        {
            Logger.LogDebug($"Iniciando proceso de actualizacion de información");
            ExamenAdmision examenadmision = await this.DbContext.ExamenAdmision.FirstOrDefaultAsync(ct => ct.ExamenId == id);
            if (examenadmision == null)
            {
                Logger.LogWarning($"No existen registros en la base de datos con el id {id}");

                return NotFound();
            }
            examenadmision.FechaExamen = value.FechaExamen;
            this.DbContext.Entry(examenadmision).State = EntityState.Modified;
            await this.DbContext.SaveChangesAsync();
            Logger.LogInformation("Se ejecuto de forma exitosa la actualización del registro");

            return NoContent();
        }
    }
}