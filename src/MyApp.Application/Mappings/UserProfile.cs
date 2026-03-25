
using MyApp.Application.Features.Identity.DTOs;
using MyApp.Domain.Abstractions;

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
