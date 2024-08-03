﻿using AutoMapper;
using INWalks.API.Models.DTO;
using INWalks.APIs.Models.Domain;

namespace INWalks.APIs.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        //Used for Object to object mapping.
        //To use mention it in program.cs using AddAutoMapper()
        //we will have to mention Source and destination class in CreateMap<>.
        //If we want vice versa mention ReverseMap().
        public AutoMapperProfiles() 
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
        }
    }
}