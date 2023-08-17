using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KalumManagement.Dtos;
using KalumManagement.Entities;

namespace KalumManagement.Utilities
{
    public class AutoMapperProfiles : Profile 
    {
        public AutoMapperProfiles()
        {
            CreateMap<Aspirante,CarreraTecnicaAspiranteListDTO>();
            CreateMap<CarreraTecnica,CarreraTecnicaListDTO>();
            CreateMap<CarreraTecnica,CarreraTecnicaCreateDTO>();
            CreateMap<CarreraTecnicaCreateDTO, CarreraTecnica>();
            CreateMap<Aspirante,JornadaAspiranteListDTO>();
            CreateMap<AspiranteCreateDTO, Aspirante>();
            CreateMap<Aspirante, AspiranteDTO>().ConstructUsing(e => new AspiranteDTO {Nombrecompleto = $"{e.Apellidos} {e.Nombres}"});
            CreateMap<ExamenAdmision, ExamenAdmisionCreateDTO>();
            CreateMap<Jornada,JornadaListDTO>();
            CreateMap<Jornada,JornadaCreateDTO>();
            CreateMap<ResultadoExamenAdmision, ResultadoExamenAdmisionListDTO>();

        

        }
    }
}