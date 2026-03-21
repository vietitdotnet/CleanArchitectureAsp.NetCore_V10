using MyApp.Domain.Contants;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Paginations.Parameters
{
    public class UserParameters : PagingParameters
    {
        [StringLength(20)]
        public string? KeySearch { get; set; }

        public override void Normalize()
        {
            base.Normalize();

            KeySearch = string.IsNullOrWhiteSpace(KeySearch) ? null : KeySearch.Trim();
           
        }

    }
}
