using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Paginations.Enum
{
    public enum SortOrder
    {
        None = 0,

        [Display(Name = "asc")]
        Ascending = 1,

        [Display(Name = "desc")]
        Descending = 2        
    }
}
