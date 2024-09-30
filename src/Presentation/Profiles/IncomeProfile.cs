using AutoMapper;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Presentation.DTOs;

namespace ExpenseTrackerGrupo4.src.Presentation.Profiles;

public class IncomeProfile : Profile
{
    public IncomeProfile()
    {
        CreateMap<CreateUpdateIncomeDto, Income>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));
    }
}
