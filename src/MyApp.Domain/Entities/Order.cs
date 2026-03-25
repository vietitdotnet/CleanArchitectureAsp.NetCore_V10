using MyApp.Domain.Abstractions;
using MyApp.Domain.Core.Models;
using MyApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Domain.Entities
{
    public class Order : BaseEntity<int>
    {
        private Order() { }

        public Order(string createdByUserId)
        {
            CreatedByUserId = createdByUserId;
            OrderDate = DateTime.UtcNow;
            Status = OrderStatus.Pending;
        }

        public string? Description { get; set; }

        public DateTime OrderDate { get; private set; }

        public OrderStatus Status { get; private set; }

        public string CreatedByUserId { get; private set; } = null!;

        // Navigation
        public IAppUserReference User { get; private set; } = null!;

        public ICollection<OrderProduct> OrderProducts { get; } = [];


        public decimal GetTotalPrice()
        {
            return OrderProducts.Sum(op => op.Product.Price * op.Quantity);
        }
         public int GetTotalCount()
        {
            return OrderProducts.Sum(op => op.Quantity);
        }
         public void UpdateStatus(OrderStatus newStatus)
        {
            // Validate status transition if needed
            Status = newStatus;
        }
    }

}
