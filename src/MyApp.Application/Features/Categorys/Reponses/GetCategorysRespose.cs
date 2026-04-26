using MyApp.Application.Features.Categorys.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Categorys.Reponses
{
    public class GetCategorysRespose
    {
        public IReadOnlyList<CategoryDto> Categories { get; private set; }


        public GetCategorysRespose(IReadOnlyList<CategoryDto> categories)
        {
            Categories = categories;
        }
    }
}
