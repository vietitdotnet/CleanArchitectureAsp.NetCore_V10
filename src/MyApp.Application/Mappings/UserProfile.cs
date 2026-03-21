
using MyApp.Application.Features.Managers.DTOs;
using MyApp.Domain.Abstractions.Users;


namespace MyApp.Application.Mappings
{
    public class UserProfile : BaseProfile
    {
        public UserProfile()
        {
            CreateMap<IAppUserReference, UserDto>();
/*            .ForMember(d => d.FullName, o => o.MapFrom(s => (s.FirstName ?? "") + " " + (s.LastName ?? "")));
*/
        }
    }
}
