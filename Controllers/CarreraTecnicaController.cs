using Microsoft.AspNetCore.Mvc;
using KalumManagement.Entities;
using Microsoft.EntityFrameworkCore;
using KalumManagement.Dtos;
using KalumManagement.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace KalumManagement.Controllers
{

    [ApiController]
    [Route("kalum-management/v1/carreras-tecnicas")]
    public class CarreraTecnicaController : ControllerBase
    {
        private readonly KalumDBContext DBContext;

        private readonly ILogger<CarreraTecnicaController> Logger;

        private readonly IMapper Mapper;

        public CarreraTecnicaController(KalumDBContext _dBContext, ILogger<CarreraTecnicaController> _logger, IMapper _mapper)
        {
            this.DBContext = _dBContext;
            this.Logger = _logger;
            this.Mapper = _mapper;
        }

        [HttpGet("page/{page}")]
        public async Task<ActionResult<IEnumerable<CarreraTecnica>>> GetCarreraTecnicaPagination(int page)
        {
            var queryable = this.DBContext.CarreraTecnica.AsQueryable();
            int registros = await queryable.CountAsync();
            if(registros == 0)
            {
                return NoContent();
            }
            else
            {       
                var carrerasTecnicas  = await queryable.OrderBy(carrerasTecnicas => carrerasTecnicas.Nombre).Pagination(page).ToListAsync();       
                PaginationResponse<CarreraTecnica> response = new PaginationResponse<CarreraTecnica>(carrerasTecnicas,page,registros);
                return Ok(response);
            
            }

        

        }
 
        [HttpGet]
        //[ResponseCache(Duration = 25)]    
        [AllowAnonymous]    
        public async Task<ActionResult<IEnumerable<CarreraTecnicaListDTO>>> Get()
        {
            Logger.LogDebug("iniciando proceso de consulta");
            List<CarreraTecnica> carreras = await DBContext.CarreraTecnica.ToListAsync();
            Logger.LogDebug("finalizando el proceso de consulta");
            if (carreras == null || carreras.Count == 0)
            {
                Logger.LogWarning("No existen registros en la base de datos");
                return new NoContentResult();
            }

            Logger.LogInformation("Se ejecuto de forma exitosa la consulta de la informacion");
            List<CarreraTecnicaListDTO> lista = this.Mapper.Map<List<CarreraTecnicaListDTO>>(carreras);
            return Ok(lista);

        }

        [HttpGet("{id}", Name = "GetCarreraTecnica")]

        public async Task<ActionResult<CarreraTecnica>> GetCarreraTecnica(String id)
        {
            Logger.LogDebug($"iniciando proceso de consulta con id {id}");
            var carrera = await DBContext.CarreraTecnica.FirstOrDefaultAsync(ct => ct.CarreraId == id);
            if (carrera == null)
            {
                Logger.LogWarning($"No existen registros en la base de datos{id}");
                return new NoContentResult();
            }

            Logger.LogInformation("Se ejecuto de forma exitosa la consulta de la informacion");
            return Ok(carrera);

        }

        [HttpPost]
        public async Task<ActionResult<CarreraTecnica>> Post([FromBody] CarreraTecnica value)
        {
            Logger.LogDebug($"Iniciando proceso de registro con la siguiente informacion {value}");
            value.CarreraId = Guid.NewGuid().ToString().ToUpper();
            Logger.LogDebug($"Generación de llave con valor {value.CarreraId}");
            await DBContext.CarreraTecnica.AddAsync(value);
            await this.DBContext.SaveChangesAsync();
            Logger.LogInformation($"Se ejecuto exitosamente el proceso de almacenamiento");
            return new CreatedAtRouteResult("GetCarreraTecnica", new { id = value.CarreraId }, value);

        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<CarreraTecnica>> Delete(string id)
        {
            Logger.LogDebug($"Iniciando proceso de registro con la siguiente informacion {id}");
            CarreraTecnica carreraTecnica = await this.DBContext.CarreraTecnica.FirstOrDefaultAsync(ct => ct.CarreraId == id);
            if (carreraTecnica == null)
            {
                Logger.LogWarning($"No existen registros en la base de datos con el id {id}");
                return NotFound();
            }

            else
            {
                this.DBContext.CarreraTecnica.Remove(carreraTecnica);
                await this.DBContext.SaveChangesAsync();
                Logger.LogInformation("Se ejecuto de forma exitosa la eliminación del registro");
                return carreraTecnica;
            }

        }

        [HttpPut("{id}")]

        public async Task<ActionResult> Put(string id, [FromBody] CarreraTecnicaCreateDTO value)
        {
            Logger.LogDebug($"Iniciando proceso de actualizacion de información");
            CarreraTecnica carreraTecnica = await this.DBContext.CarreraTecnica.FirstOrDefaultAsync(ct => ct.CarreraId == id);
            if (carreraTecnica == null)
            {
                Logger.LogWarning($"No existen registros en la base de datos con el id {id}");

                return NotFound();
            }
            carreraTecnica.Nombre = value.Nombre;
            this.DBContext.Entry(carreraTecnica).State = EntityState.Modified;
            await this.DBContext.SaveChangesAsync();
            Logger.LogInformation("Se ejecuto de forma exitosa la actualización del registro");

            return NoContent();
        }




    }
}