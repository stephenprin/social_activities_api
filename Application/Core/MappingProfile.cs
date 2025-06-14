using System.Diagnostics;
using Application.Activities.Command;
using AutoMapper;

namespace Application.Core;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Activity, Activity>();
        CreateMap<Activity, ActivityDto>();

    }
}
