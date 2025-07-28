using AutoMapper;
using ClientesCRM.src.Application.Commands.ClienteCommands;
using ClientesCRM.src.Application.ApplicationDTOs.ClienteDTOs;
using ClientesCRM.src.Core.Entities;

namespace ClientesCRM.src.Application.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Comando → Entidad
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

            // Entidad → DTO de respuesta
            CreateMap<Cliente, ClienteResponseDto>()
                .ForMember(d => d.Id,
                       o => o.MapFrom(src => src.Id))
                .ForMember(dest => dest.direccionPrincipal,
                           opt => opt.MapFrom(src => src.DireccionPrincipal));
        }
    }
}
