using AutoMapper;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Presentation.DTOs;

namespace ExpenseTrackerGrupo4.src.Presentation.Profiles;

public class ExpenseTrackerProfile : Profile
{
    public ExpenseTrackerProfile()
    {
        CreateMap<RegisterRequestDTO, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
    }
}
