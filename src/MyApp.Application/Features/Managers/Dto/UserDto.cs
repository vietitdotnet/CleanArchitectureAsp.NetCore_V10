

using MyApp.Domain.Core.Models;

namespace MyApp.Application.Features.Managers.DTOs
{
    public class UserDto : BaseDto
    {      
        public string? LastName { get; private set; }
    
        public string? FirstName { get; private set; }

        public string Email { get; private set; } =  null!;

        public string? FullName => $"{FirstName} {LastName}".Trim();

    }
}
