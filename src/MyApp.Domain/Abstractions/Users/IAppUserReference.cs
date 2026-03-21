using MyApp.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Abstractions.Users
{
    public interface IAppUserReference 
    {
         string Id { get; }
         string? FirstName { get; }
         string? LastName { get; }
         string? Email { get; }
         DateTime? BirthDate { get; }
         string? Company { get; }

         [DataType(DataType.Text)]
         string? Description { get; }
         string? Avartar { get; }

    }
}
