
using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Paginations.Parameters
{
    public class UserParameters : PagingParameters
    {
        protected override int DefaultPageSize => 10;

        [StringLength(20)]
        public string? KeySearch { get; set; }

        public override void Normalize()
        {
            base.Normalize();
            KeySearch = string.IsNullOrWhiteSpace(KeySearch) ? null : KeySearch.Trim();
        }
    }

}
