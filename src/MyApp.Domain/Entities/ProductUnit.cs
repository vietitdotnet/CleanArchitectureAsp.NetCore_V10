using MyApp.Domain.Core.Models;

namespace MyApp.Domain.Entities
{
    public class ProductUnit : BaseEntity<int>
    {
        private ProductUnit() { }

        public Guid PublicId { get; private set; }

        public int ProductId { get; private set; }

        public string? Barcode { get; private set; }

        public string UnitName { get; private set; } = null!;
  
        //Ví dụ: 1 thùng = 24 đơn vị base
        public int ConversionRate { get; private set; }

        public decimal SellingPrice { get; private set; }

        // Đơn vị gốc (chai, cái...)
        // Mặc định không phải đơn vị gốc ,
        // nếu là đơn vị gốc thì conversion rate = 1 ,
        // chỉ có 1 đơn vị gốc duy nhất cho mỗi sản phẩm
        public bool IsBaseUnit { get; private set; } = false;

        // Khi tạo mới sẽ không active ngay, phải đợi admin duyệt
        public bool IsActive { get; private set; } = false;

        public Product Product { get; private set; } = null!;

        private readonly List<PromotionItem> _promotionItems = new();
        public IReadOnlyCollection<PromotionItem> PromotionItems => _promotionItems.AsReadOnly();


        private ProductUnit(
          int productId,
          string unitName,
          int conversionRate,
          decimal sellingPrice,
          bool isBaseUnit,
          string? barcode)
        {
            ProductId = productId;
            UnitName = unitName;
            ConversionRate = conversionRate;
            SellingPrice = sellingPrice;
            IsBaseUnit = isBaseUnit;
            Barcode = barcode;
        }

        // ===== Factory =====

        public static ProductUnit Create(
            int productId,
            string unitName,
            int conversionRate,
            decimal sellingPrice,
            bool isBaseUnit = false,
            string? barcode = null)
        {
            if (productId <= 0)
                throw new ArgumentException("ProductId must be valid");

            if (string.IsNullOrWhiteSpace(unitName))
                throw new ArgumentException("Unit name is required");

            if (conversionRate <= 0)
                throw new ArgumentException("Conversion rate must be > 0");

            if (sellingPrice < 0)
                throw new ArgumentException("Selling price must be >= 0");

            // nếu là base unit → conversion = 1
            if (isBaseUnit)
                conversionRate = 1;

            return new ProductUnit(
                productId,
                unitName.Trim(),
                conversionRate,
                sellingPrice,
                isBaseUnit,
                barcode?.Trim());
        }


        public void AddPromotionItem(
                  int PromotionId,
                  ProductUnit productUnit,
                  decimal? overrideValue = null,
                  bool? isPercentageOverride = null)
        {
            if (productUnit == null)
                throw new ArgumentNullException(nameof(productUnit));

            // Check đã tồn tại chưa
            var existing = _promotionItems.FirstOrDefault(x => x.ProductUnitId == productUnit.Id);

            if (existing != null)
            {
                // UPDATE override nếu đã có
                existing.UpdateOverride(overrideValue, isPercentageOverride);
                return;
            }

            // Tạo mới PromotionItem
            var item = PromotionItem.Create(
                promotionId: PromotionId,
                productUnitId: productUnit.Id,
                productName: productUnit.Product.Name,
                unitName: productUnit.UnitName,
                originalPrice: productUnit.SellingPrice,
                unitBarcode: productUnit.Barcode,
                overrideValue: overrideValue,
                isPercentageOverride: isPercentageOverride
            );

            _promotionItems.Add(item);
        }

  
        // ===== Behavior =====

        public void Activate()
        {
            IsActive = true;
        }

        public void UpdateUnitName(string unitName)
        {
            if (string.IsNullOrWhiteSpace(unitName))
                throw new ArgumentException("Unit name is required");
            UnitName = unitName.Trim();
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice < 0)
                throw new ArgumentException("Price must be >= 0");

            SellingPrice = newPrice;
        }

        public void UpdateBarcode(string? barcode)
        {
            Barcode = string.IsNullOrWhiteSpace(barcode)
                ? null
                : barcode.Trim();
        }

        public void SetAsBaseUnit()
        {
            IsBaseUnit = true;
            ConversionRate = 1;
        }

        public void UpdateConversionRate(int conversionRate)
        {
            if (conversionRate <= 0)
                throw new ArgumentException("Conversion rate must be > 0");

            if (IsBaseUnit)
                throw new InvalidOperationException("Base unit must have conversion rate = 1");

            ConversionRate = conversionRate;
        }

        public void Rename(string unitName)
        {
            if (string.IsNullOrWhiteSpace(unitName))
                throw new ArgumentException("Unit name is required");

            UnitName = unitName.Trim();
        }

        public bool IsValid(PromotionItem promo)
        {
            var p = promo.Promotion;

            if (!p.IsActive) return false;

            if (DateTime.Now < p.StartDate || DateTime.Now > p.EndDate)
                return false;

            return true;

        }
    }

}
