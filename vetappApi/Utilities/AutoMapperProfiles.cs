
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD
using vetappApi.DTOs;
=======
>>>>>>> origin/main
using vetappback.DTOs;
using vetappback.Entities;

namespace vetappback.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Pet,PetDTO>().ReverseMap();
            CreateMap<Pet,PetDTO>();
            CreateMap<PetCreateDTO,Pet>();
            
            CreateMap<History,HistoryDTO>().ReverseMap();
            CreateMap<History,HistoryDTO>();
            CreateMap<HistoryCreateDTO,History>();
         
            CreateMap<Agenda,AgendaDTO>().ReverseMap();
            CreateMap<Agenda,AgendaDTO>();
            CreateMap<AgendaCreateDTO,Agenda>();

<<<<<<< HEAD
            CreateMap<ServiceTypeCreateDTO,ServiceType>();

=======
>>>>>>> origin/main
        }
    }
}