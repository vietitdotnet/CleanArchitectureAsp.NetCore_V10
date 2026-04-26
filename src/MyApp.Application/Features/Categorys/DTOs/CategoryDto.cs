using MyApp.Domain.Core.Models;
using MyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Categorys.DTOs
{
    public class CategoryDto : BaseDto
    {
        public int Id { get; set; }
        public string Name { get;  set; } = null!;

        public string Slug { get;  set; } = null!;

        public int? ParentCategoryId { get;  set; }

        public ICollection<CategoryDto> CategoryChildrens { get; set; } = [];

        
    }
}
