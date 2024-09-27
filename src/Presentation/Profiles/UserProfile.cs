using AutoMapper;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Presentation.DTOs;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserUpdateRequestDTO, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
    }
}