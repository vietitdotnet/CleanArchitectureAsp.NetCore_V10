
using MyApp.Domain.Core.Models;

namespace MyApp.Domain.Entities
{
    public class OrderItem : BaseEntity<int>
    {
        private OrderItem() { }

        public int OrderId { get; private set; }
        public Order Order { get; private set; } = null!;
        public int ProductUnitId { get; private set; }
        public ProductUnit ProductUnit { get; private set; } = null!;
        public string ProductSku { get; private set; } = null!; // snapshot, lưu lại mã sản phẩm tại thời điểm đặt hàng, để đảm bảo tính toán giá chính xác ngay cả khi sau này mã sản phẩm có thể thay đổi
        public string ProductName { get; private set; } = null!;
        public string UnitName { get; private set; } = null!;
        public string ? UnitBarcode { get; private set; } // snapshot, lưu lại mã vạch của đơn vị sản phẩm tại thời điểm đặt hàng, để đảm bảo tính toán giá chính xác ngay cả khi sau này mã vạch có thể thay đổi
        public int Quantity { get; private set; } // số lượng sản phẩm trong đơn hàng, có thể khác với số lượng tồn kho của sản phẩm nếu sau này có thay đổi về tồn kho
        public decimal UnitPrice { get; private set; } // snapshot, giá bán tại thời điểm đặt hàng, có thể khác với giá hiện tại của sản phẩm nếu sau này giá sản phẩm thay đổi
        public decimal VatPercentage { get; private set; } // snapshot, lưu lại phần trăm thuế VAT tại thời điểm đặt hàng, để đảm bảo tính toán giá chính xác ngay cả khi sau này thuế VAT thay đổi
        public decimal DiscountAmount { get; private set; }// snapshot, lưu lại số tiền chiết khấu đã áp dụng cho sản phẩm này tại thời điểm đặt hàng, để đảm bảo tính toán giá chính xác ngay cả khi sau này có thêm các chương trình khuyến mãi khác
        public decimal? DiscountPercentage { get; private set; } // nếu có %
        public string? DiscountType { get; private set; } // FlashSale / Promotion / Voucher
        public string? DiscountCode { get; private set; } // mã KM (nếu có)

        public decimal TotalPrice { get; private set; } // snapshot, giá trị cuối cùng của sản phẩm trong đơn hàng sau khi đã tính toán thuế VAT và chiết khấu, được lưu lại để đảm bảo tính toán giá chính xác ngay cả khi sau này có thay đổi về giá sản phẩm, thuế VAT hoặc các chương trình khuyến mãi khác


        public OrderItem(
            int productId,
            string unitName,
            decimal unitPrice,
            int quantity,
            decimal vatPercentage,
            decimal discountAmount = 0)
        {
            ProductUnitId = productId;
            UnitName = unitName;
            UnitPrice = unitPrice;
            Quantity = quantity;
            VatPercentage = vatPercentage;
            DiscountAmount = discountAmount;

            Recalculate();
        }

        public void SetProductInfo(string productSku, string productName)
        {
            ProductSku = productSku;
            ProductName = productName;
        }

        public static OrderItem Create(
            int productId,
            string unitName,
            decimal unitPrice,
            int quantity,
            decimal vatPercentage,
            decimal discountAmount = 0)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be > 0");
            if (unitPrice < 0)
                throw new ArgumentException("Unit price must be >= 0");
            if (vatPercentage < 0)
                throw new ArgumentException("VAT percentage must be >= 0");
            if (discountAmount < 0)
                throw new ArgumentException("Discount amount must be >= 0");

            return new OrderItem(productId, unitName, unitPrice, quantity, vatPercentage, discountAmount);
        }

        public void UpdateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be > 0");

            Quantity = quantity;
            
            Recalculate();
        }

        public void ApplyDiscount(decimal discount)
        {
            if (discount < 0)
                throw new ArgumentException("Discount invalid");

            DiscountAmount = discount;
            Recalculate();
        }

        private void Recalculate()
        {
            var subtotal = UnitPrice * Quantity; // giá trị trước thuế và chiết khấu

            var vat = subtotal * VatPercentage / 100; // số tiền thuế VAT

            TotalPrice = subtotal + vat - DiscountAmount; // giá trị cuối cùng sau khi đã cộng thuế và trừ chiết khấu

            if (TotalPrice < 0)
                TotalPrice = 0;
        }

        public void ApplyPromotion(Promotion promotion)
        {
            if (!promotion.IsActive)
                return;

            var subtotal = UnitPrice * Quantity;

            decimal discount = 0;

            if (promotion.IsPercentage)
            {
                discount = subtotal * promotion.Value / 100;

                if (promotion.MaxDiscountAmount.HasValue)
                    discount = Math.Min(discount, promotion.MaxDiscountAmount.Value);
            }
            else
            {
                discount = promotion.Value;
            }

            ApplyDiscount(discount);
        }
    }


}
