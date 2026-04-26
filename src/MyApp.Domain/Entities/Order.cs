using MyApp.Domain.Abstractions;
using MyApp.Domain.Core.Models;
using MyApp.Domain.Entities.Owns;
using MyApp.Domain.Enums;

namespace MyApp.Domain.Entities
{
    public class Order : BaseEntity<int>
    {
        private Order() { }

        public Guid PublicId { get; private set; }
        public string? Note { get; private set; }

        public DateTimeOffset OrderDate { get; private set; }

        public OrderStatus Status { get; private set; }

        public string? CreatedByUserId { get; private set; }

        public IAppUserReference? User { get; private set; }

        public ShippingAddress Address { get; private set; } = null!;

        public AnonCustomer Customer { get; private set; } = null!;

        public ICollection<OrderItem> OrderItems { get; } = [];

        private Order(string? createdByUserId)
        {
            CreatedByUserId = createdByUserId;
            Status = OrderStatus.Pending;
        }

        public static Order Create(string? userId)
        {
            return new Order(userId);
        }

        public void SetNote(string? note)
        {
            Note = string.IsNullOrWhiteSpace(note) ? null : note.Trim();
        }

        public void SetCustomer(AnonCustomer customer)
        {
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        }

        public void SetShippingAddress(ShippingAddress address)
        {
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public void AddProduct(ProductUnit unit, int quantity)
        {
            

            if (unit == null)
                throw new ArgumentNullException(nameof(unit));

            if (quantity <= 0)
                throw new ArgumentException("Số lượng phải lớn hơn 0");

            var existing = OrderItems
                .FirstOrDefault(x => x.ProductUnitId == unit.Id && x.UnitName == unit.UnitName);

            if (existing != null)
            {
                existing.UpdateQuantity(existing.Quantity + quantity);
                return;
            }

            OrderItems.Add(new OrderItem(
                unit.Id,
                unit.UnitName,
                unit.SellingPrice,
                quantity,
                unit.Product.Tax?.Percentage ?? 0
            ));
        }

        public void UpdateStatus(OrderStatus newStatus)
        {
            // Có thể thêm rule chuyển trạng thái sau
            Status = newStatus;
        }

        public decimal GetTotalPrice()
        {
            return OrderItems.Sum(op => op.TotalPrice);
        }

        public int GetTotalCount()
        {
            return OrderItems.Sum(op => op.Quantity);
        }

        // Validation trước khi save
        public void Validate()
        {
            if (!OrderItems.Any())
                throw new InvalidOperationException("Đơn hàng phải có ít nhất một sản phẩm.");

            if (Address == null)
                throw new InvalidOperationException("Địa chỉ giao hàng là bắt buộc");

            if (Customer == null)
                throw new InvalidOperationException("Thông tin khách hàng là bắt buộc");
        }
    }


}
