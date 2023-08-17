using KalumManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using KalumManagement.Dtos;
using KalumManagement.Utilities;
using AutoMapper;

namespace KalumManagement.Controllers
{
    [ApiController]
    [Route("kalum-management/v1/resultados-examen-admision")]
    public class ResultadoExamenAdmisionController : ControllerBase
    {
        private readonly KalumDBContext DbContext;
        private readonly ILogger<ResultadoExamenAdmisionController> Logger;
        private readonly IMapper Mapper;

        

        public ResultadoExamenAdmisionController(KalumDBContext _dBContext, ILogger<ResultadoExamenAdmisionController> _logger, IMapper _mapper)
        {
            this.DbContext = _dBContext;
            this.Logger = _logger;
            this.Mapper = _mapper;
        }


        [HttpGet("page/{page}")]
        public async Task<ActionResult<IEnumerable<ResultadoExamenAdmision>>> GetResultadoExamenAdmisionPagination(int page)
        {
            var queryable = this.DbContext.ResultadoExamenAdmision.AsQueryable();
            int registros = await queryable.CountAsync();
            if (registros == 0)
            {
                return NoContent();
            }
            else
            {
                var resultadoexamenadmision = await queryable.OrderBy(ResultadoExamenAdmision => ResultadoExamenAdmision.NoExpediente).Pagination(page).ToListAsync();
                PaginationResponse<ResultadoExamenAdmision> response = new PaginationResponse<ResultadoExamenAdmision>(resultadoexamenadmision, page, registros);
                return Ok(response);

            }
        }

         [HttpGet]
        //[ResponseCache(Duration = 25)]    
        public async Task<ActionResult<IEnumerable<ResultadoExamenAdmisionDTO>>> Get()
        {
            Logger.LogDebug("iniciando proceso de consulta");
            List<ResultadoExamenAdmision> resultados = await DbContext.ResultadoExamenAdmision.ToListAsync();
            Logger.LogDebug("finalizando el proceso de consulta");
            if (resultados == null || resultados.Count == 0)
            {
                Logger.LogWarning("No existen registros en la base de datos");
                return new NoContentResult();
            }

            Logger.LogInformation("Se ejecuto de forma exitosa la consulta de la informacion");
            List<ResultadoExamenAdmisionListDTO> lista = this.Mapper.Map<List<ResultadoExamenAdmisionListDTO>>(resultados);
            return Ok(lista);

        }


        [HttpPost]
        public async Task<ActionResult<ResultadoExamenAdmision>> Post([FromBody] ResultadoExamenAdmision value)
        {
            Logger.LogDebug($"Iniciando proceso de registro con la siguiente informacion {value}");
            value.NoExpediente = Guid.NewGuid().ToString().ToUpper();
            Logger.LogDebug($"Generación de llave con valor {value.NoExpediente}");
            await DbContext.ResultadoExamenAdmision.AddAsync(value);
            await this.DbContext.SaveChangesAsync();
            Logger.LogInformation($"Se ejecuto exitosamente el proceso de almacenamiento");
            return new CreatedAtRouteResult("GetResultadoExamenAdmision", new { id = value.NoExpediente }, value);

        }


        [HttpDelete("{id}")]

        public async Task<ActionResult<ResultadoExamenAdmision>> Delete(string id)
        {
            Logger.LogDebug($"Iniciando proceso de registro con la siguiente informacion {id}");
            ResultadoExamenAdmision resultadoExamen = await this.DbContext.ResultadoExamenAdmision.FirstOrDefaultAsync(rs => rs.NoExpediente == id);
            if (resultadoExamen == null)
            {
                Logger.LogWarning($"No existen registros en la base de datos con el id {id}");
                return NotFound();
            }

            else
            {
                this.DbContext.ResultadoExamenAdmision.Remove(resultadoExamen);
                await this.DbContext.SaveChangesAsync();
                Logger.LogInformation("Se ejecuto de forma exitosa la eliminación del registro");
                return resultadoExamen;
            }

        }


         [HttpPut("{id}")]

        public async Task<ActionResult> Put(string id, [FromBody] ResultadoExamenAdmision value)
        {
            Logger.LogDebug($"Iniciando proceso de actualizacion de información");
            ResultadoExamenAdmision resultadoExamen = await this.DbContext.ResultadoExamenAdmision.FirstOrDefaultAsync(rs => rs.NoExpediente == id);
            if (resultadoExamen == null)
            {
                Logger.LogWarning($"No existen registros en la base de datos con el id {id}");

                return NotFound();
            }
            resultadoExamen.Nota = value.Nota;
            this.DbContext.Entry(resultadoExamen).State = EntityState.Modified;
            await this.DbContext.SaveChangesAsync();
            Logger.LogInformation("Se ejecuto de forma exitosa la actualización del registro");

            return NoContent();
        }


    }


}