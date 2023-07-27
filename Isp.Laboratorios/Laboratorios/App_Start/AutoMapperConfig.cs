using System;
using AutoMapper;
using Isp.Laboratorios.Infrastructure;
using Isp.Laboratorios.Models;
using Isp.Laboratorios.Models.ViewModels;

namespace Isp.Laboratorios
{
    public static class AutoMapperConfig
    {

        public static void RegisterMapper()
        {

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Muestra, MuestraViewModel>()
                   .ForMember(destino => destino.ProcedenciaNombre, o => o.MapFrom(fuente => fuente.Solicitud.Procedencia.Nombre));

                cfg.CreateMap<Examen, ExamenViewModel>()
                    .ForMember(destino => destino.LaboratorioNombre, o => o.MapFrom(fuente => fuente.Prestacion.Laboratorio.Nombre))
                    .ForMember(destino => destino.PrestacionDias,
                    o => o.MapFrom(fuente => fuente.Muestra.Solicitud.Fecha.BusinessDaysUntil(DateTime.Now)
                       ));

                cfg.CreateMap<ResultadoAnalito, ResultadoAnalitoViewModel>();

                cfg.CreateMap<ResultadoAnalito, BitacoraResultadoAnalito>();
            });
        }


    }
}