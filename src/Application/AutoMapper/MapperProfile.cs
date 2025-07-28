using AutoMapper;
using ClientesCRM.src.Application.ApplicationDTOs.ClienteDTOs;
using ClientesCRM.src.Application.Commands.ClienteCommands;
using ClientesCRM.src.Core.Entities;

namespace ClientesCRM.src.Application.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // 1) DTO de creación → Comando 
            CreateMap<ClienteCreateDto, ClienteCreateCommand>()
            .ConstructUsing(dto => new ClienteCreateCommand(
                dto.Nombre,
                dto.Apellido,
                dto.Telefono,
                dto.CorreoElectronico,
                dto.DireccionPrincipal,
                dto.NotasGenerales
            ));


            // 2) Comando → Entidad de dominio 
            CreateMap<ClienteCreateCommand, Cliente>()
                .ConstructUsing(cmd =>
                    new Cliente(
                        cmd.Nombre,
                        cmd.Apellido,
                        cmd.Telefono,
                        cmd.CorreoElectronico,
                        cmd.DireccionPrincipal,
                        cmd.NotasGenerales
                    )
                );

            // 3) Entidad → DTO de respuesta
            CreateMap<Cliente, ClienteResponseDto>()
                .ForMember(d => d.Id,
                           o => o.MapFrom(src => src.Id))
                .ForMember(d => d.Nombre,
                           o => o.MapFrom(src => src.Nombre))
                .ForMember(d => d.Apellido,
                           o => o.MapFrom(src => src.Apellido))
                .ForMember(d => d.Telefono,
                           o => o.MapFrom(src => src.Telefono))
                .ForMember(d => d.direccionPrincipal,
                           o => o.MapFrom(src => src.DireccionPrincipal))
                .ForMember(d => d.NotasGenerales,
                           o => o.MapFrom(src => src.NotasGenerales));
        }
    }
}

