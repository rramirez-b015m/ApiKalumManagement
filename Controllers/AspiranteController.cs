
using KalumManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using KalumManagement.Dtos;
using AutoMapper;
using KalumManagement.Services;
using KalumManagement.Models;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace KalumManagement.Controllers
{

    [Route("kalum-management/v1/aspirante")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class AspiranteController : ControllerBase
    {

        private readonly KalumDBContext DbContext;
        private readonly ILogger<JornadaController> Logger;

        private readonly IMapper Mapper;

        private readonly IQueueService QueueService;

        public AspiranteController(KalumDBContext _dBContext, ILogger<JornadaController> _logger, IMapper _mapper, IQueueService _queueservice)
        {
            this.DbContext = _dBContext;
            this.Logger = _logger;
            this.Mapper = _mapper;
            this.QueueService = _queueservice;
        }


        [HttpPost]
        public async Task<ActionResult<AspiranteDTO>> Post([FromBody] AspiranteCreateDTO aspiranteCreateDTO)
        {
            Logger.LogDebug($"Iniciando proceso de registro con la siguiente informacion {aspiranteCreateDTO}");
            CarreraTecnica carreraTecnica = await this.DbContext.CarreraTecnica.FirstOrDefaultAsync(ct => ct.CarreraId == aspiranteCreateDTO.CarreraId);

            if (carreraTecnica == null)
            {
                this.Logger.LogWarning($"No existe la carrera tecnica con el id {aspiranteCreateDTO.CarreraId}");
                return BadRequest();
            }


            Jornada jornada = await this.DbContext.Jornada.FirstOrDefaultAsync(j => j.JornadaId == aspiranteCreateDTO.JornadaId);

            if (jornada == null)
            {
                this.Logger.LogWarning($"No existe la jornada con el id {aspiranteCreateDTO.JornadaId}");
                return BadRequest();
            }


            ExamenAdmision examenAdmision = await this.DbContext.ExamenAdmision.FirstOrDefaultAsync(ex => ex.ExamenId == aspiranteCreateDTO.ExamenId);

            if (examenAdmision == null)
            {
                this.Logger.LogWarning($"No existe el examen de admision con el id {aspiranteCreateDTO.ExamenId}");
                return BadRequest();
            }

            bool resultado = await this.QueueService.CrearSolicitudAspiranteAsync(aspiranteCreateDTO);
            this.Logger.LogInformation("Se finalizo el proceso de registro de un aspirante nuevo");
            AspiranteResponse aspiranteResponse = null;
            if (resultado)
            {
                aspiranteResponse = new AspiranteResponse()
                {
                    Estatus = "OK",
                    Mensaje = $"El proceso de su solicitud fue enviada con exito, pronto recibirá la respuesta al correo {aspiranteCreateDTO.Email}"

                };

            }
            else
            {
                aspiranteResponse = new AspiranteResponse()
                {
                    Estatus = "ERROR",
                    Mensaje = "Hubo un error en su solicitud, favor de contactar con el administrador"
                };

            }
            return Ok(aspiranteResponse);
        }


        [HttpGet("{noExpediente}")]

        public async Task<ActionResult<AspiranteDTO>> getAspiranteByExpediente(string noExpediente)
        {

            Aspirante aspirante = await this.DbContext.Aspirante.Include(a => a.ExamenAdmision).Include(a => a.CarreraTecnica).FirstOrDefaultAsync(a => a.NoExpediente == noExpediente);
            if (aspirante == null)
            {
               return new NoContentResult();
            }

            else
            {
              return Ok(this.Mapper.Map<AspiranteDTO>(aspirante));

            }

        }




        [HttpDelete("{id}")]
        public async Task<ActionResult<Aspirante>> Delete(string id)
        {
            Logger.LogDebug($"Iniciando proceso de eliminación con id {id}");
            Aspirante aspirante = await this.DbContext.Aspirante.FirstOrDefaultAsync(a => a.NoExpediente == id);
            if (aspirante == null)
            {
                Logger.LogWarning($"No existe registro con id {id}");
                return NotFound();
            }
            else
            {
                this.DbContext.Aspirante.Remove(aspirante);
                await this.DbContext.SaveChangesAsync();
                Logger.LogDebug("Finalizando el proceso de eliminación");
                return aspirante;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Aspirante>> Put(string id, [FromBody] Aspirante value)
        {
            Logger.LogDebug($"Iniciando proceso de modificación con id {id}");
            Aspirante aspirante = await this.DbContext.Aspirante.FirstOrDefaultAsync(a => a.NoExpediente == id);
            if (aspirante == null)
            {
                Logger.LogWarning($"No existe registro con id {id}");
                return NotFound();
            }
            else
            {
                aspirante.Apellidos = value.Apellidos;
                aspirante.Nombres = value.Nombres;
                aspirante.Direccion = value.Direccion;
                aspirante.Telefono = value.Telefono;
                aspirante.Email = value.Email;
                aspirante.Estatus = value.Estatus;
                aspirante.CarreraId = value.CarreraId;
                aspirante.JornadaId = value.JornadaId;
                aspirante.ExamenId = value.ExamenId;

                this.DbContext.Entry(aspirante).State = EntityState.Modified;
                await this.DbContext.SaveChangesAsync();
                Logger.LogDebug("Finalizando el proceso de actualización");
                return NoContent();
            }
        }



    }

}