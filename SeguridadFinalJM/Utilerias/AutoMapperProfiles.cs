using AutoMapper;
using SeguridadFinalJM.DTOs;
using SeguridadFinalJM.Entidades;

namespace SeguridadFinalJM.Utilerias
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TarjetaDtoCreate, tarjeta>();
        }
    }
}
