using System.Diagnostics;
using AutoMapper;

namespace Application.Core;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Activity, Activity>();
        
    }
}
