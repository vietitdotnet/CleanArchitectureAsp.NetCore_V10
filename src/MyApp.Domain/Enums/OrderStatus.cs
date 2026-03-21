
using System.ComponentModel.DataAnnotations;

namespace MyApp.Domain.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "Chờ xử lý")]
        Pending = 1,

        [Display(Name = "Đang xử lý")]
        Processing = 2,

        [Display(Name = "Đã giao hàng")]
        Shipped = 3,

        [Display(Name = "Đã nhận hàng")]
        Delivered = 4,

        [Display(Name = "Đã hủy")]
        Cancelled = 5
    }
}
