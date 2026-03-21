using Microsoft.AspNetCore.Identity;
using MyApp.Domain.Abstractions.Users;
using MyApp.Domain.Core.Models;
using MyApp.Infrastructure.Models.Enums;
using System.ComponentModel.DataAnnotations;


namespace MyApp.Infrastructure.Models
{
    public class AppUser :  IdentityUser, IAppUserReference
    {
        
        [StringLength(50)]
        public string? LastName { get; set; }

        [StringLength(50)]
        public string? FirstName { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(50)]
        public string? Company { get; set; }

        [DataType(DataType.Text)]
        public string? Description { get; set; }

        public string? Avartar { get; set; }
        
    }
}
