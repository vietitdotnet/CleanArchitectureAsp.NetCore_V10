using MyApp.Domain.Core.Models;
using MyApp.Domain.Enums;

namespace MyApp.Domain.Entities
{
    public class Product : BaseEntity<int>
    {
       
        private Product() { }
        public string Sku { get; private set; } = null!; // mã sản phẩm
        public string? Barcode { get; private set; } // mã vạch, nếu có
        public string Slug { get; private set; } = null!; // slug dùng để tạo URL thân thiện, có thể dùng để hiển thị trong danh sách sản phẩm
        public string Name { get; private set; } = null!; // tên sản phẩm, có thể dùng để hiển thị trong danh sách sản phẩm
        public string BrandName { get; private set; } = null!; // tên thương hiệu, có thể dùng để hiển thị trong danh sách sản phẩm
        public decimal CostPrice { get; private set; }  // giá nhập (giá vốn) - giá mà nhà thuốc mua vào    
        public decimal BasePrice { get; private set; }  //giá gốc (giá niêm yết)       
        public ProductStatus Status { get; private set; } // trạng thái sản phẩm (Active, Inactive, Discontinued)
        public string? ShortDescription { get; private set; }// mô tả ngắn gọn về sản phẩm, có thể dùng để hiển thị trong danh sách sản phẩm
        public string? Description { get; private set; }// mô tả chi tiết về sản phẩm
        public string? PackingSize { get; private set; }// quy cách đóng gói
        public string? RegistrationNumber { get; private set; }// số đăng ký, nếu có
        public string? DosageForm { get; private set; } // dạng bào chế 
        public string? Ingredient { get; private set; } // thành phần hoạt chất chính
        public int StockQuantity { get; set; } // số lượng tồn kho hiện tại
        public int LowStockThreshold { get; set; } // ngưỡng cảnh báo tồn kho thấp

        public DateTimeOffset DateCreate { get; private set; }
        public DateTimeOffset? DateUpdate { get; private set; }

        public int? CategoryId { get; private set; }
        public Category? Category { get; private set; }

        public int? ManufacturerId { get; private set; } // nhà sản xuất chính của sản phẩm, nếu có
        public Manufacturer? Manufacturer { get; private set; }// thông tin chuyên biệt cho nhà sản xuất, nếu có

        public int? TaxId { get; private set; } // thuế VAT áp dụng cho sản phẩm, nếu có
        public Tax Tax { get; private set; } = null!; // thông tin chuyên biệt cho thuế, nếu có

        public Medicine? Medicine { get; private set; } // thông tin chuyên biệt cho sản phẩm là thuốc, nếu có

        public ICollection<ProductUnit> ProductUnits { get; set; } = [];
        


        private Product(string name, string slug, decimal costPrice)
        {
            Name = name.Trim();
            Slug = slug.Trim().ToLowerInvariant();
            CostPrice = costPrice;
            
        }

        public static Product Create(
            
            string name,
            string slug,
            decimal codePrice,
   
            string? description = null,
            int? categoryId = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name is required.");

            if (string.IsNullOrWhiteSpace(slug))
                throw new ArgumentException("Slug is required.");

            if (codePrice < 0)
                throw new ArgumentException("Price cannot be negative.");

            return new Product(name, slug, codePrice)
            {
                Description = description?.Trim(),
                CategoryId = categoryId
            };
        }

        public void Update(
            string name,
            string slug,
            decimal costPrice,
            string? description,
            int? categoryId)
        {

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.");

            if (costPrice < 0)
                throw new ArgumentException("Price cannot be negative.");

            Name = name.Trim();
            Slug = slug.Trim().ToLowerInvariant();
            CostPrice = costPrice;
            Description = description?.Trim();
            CategoryId = categoryId;
            DateUpdate = DateTimeOffset.UtcNow;

        }

        // Một sản phẩm có thể có nhiều đơn vị tính khác nhau,
        // ví dụ: viên, hộp, lọ, vỉ... Mỗi đơn vị sẽ có quy đổi về đơn vị cơ bản (ví dụ: 1 hộp = 10 viên)
        // và giá bán tương ứng.
        public ProductUnit AddUnit(
                 string unitName,
                 int conversionRate,
                 decimal price,
                 bool isBaseUnit = false,
                 string? barcode = null)
        {
            if (isBaseUnit && ProductUnits.Any(x => x.IsBaseUnit))
                throw new InvalidOperationException("Product already has a base unit");

            var unit = ProductUnit.Create(
                Id,
                unitName,
                conversionRate,
                price,
                isBaseUnit,
                barcode);

            ProductUnits.Add(unit);

            return unit;
        }

        public void RemoveUnit(string unitName)
        {
            var unit = ProductUnits.FirstOrDefault(x => x.UnitName == unitName);

            if (unit == null)
                throw new InvalidOperationException("Unit not found");

            ProductUnits.Remove(unit);
        }

        public void SetCodePrice(decimal newPrice)
        {
            if (newPrice < 0)
                throw new ArgumentException("New price must be positive.");

            CostPrice = newPrice;
            DateUpdate = DateTime.UtcNow;
        }

        public void SetBasePrice(decimal price)
        {
            if (price < 0)
                throw new ArgumentException("Price must be >= 0");

            BasePrice = price;
            DateUpdate = DateTime.UtcNow;
        }

        // Có thể gán sản phẩm vào nhiều tax, nhưng để đơn giản thì tạm thời mỗi sản phẩm chỉ có 1 tax chính
        public void AssignTax(int taxId)
        {
            TaxId = taxId;
            DateUpdate = DateTime.UtcNow;
        }

        // Có thể gán sản phẩm vào nhiều manufacturer, nhưng để đơn giản thì tạm thời mỗi sản phẩm chỉ có 1 manufacturer chính
        public void AssignManufacturer(int manufacturerId)
        {
            ManufacturerId = manufacturerId;
            DateUpdate = DateTime.UtcNow;
        }

        // Có thể gán sản phẩm vào nhiều category, nhưng để đơn giản thì tạm thời mỗi sản phẩm chỉ có 1 category chính
        public void AssignToCategory(int categoryId)
        {
            CategoryId = categoryId;
            DateUpdate = DateTime.UtcNow;
        }


        // Trạng thái sản phẩm: Active = đang bán, Inactive = tạm ngưng bán, Discontinued = ngừng bán vĩnh viễn
        public void Activate()
        {
            Status = ProductStatus.Active;
        }
        public void Deactivate()
        {
            Status = ProductStatus.Inactive;
        }
        public void Discontinue()
        {
            Status = ProductStatus.Discontinued;
        }


        // Khi nhập hàng, cần tăng số lượng tồn kho.
        // Nếu số lượng nhập vào là âm hoặc bằng 0, có thể ném lỗi.
        public void IncreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than 0");

            StockQuantity += quantity;
        }

        // Khi bán hàng, cần giảm số lượng tồn kho. Nếu số lượng bán ra lớn hơn số lượng tồn kho hiện tại,
        // có thể ném lỗi hoặc cho phép âm tồn kho tùy theo chính sách của nhà thuốc.
        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than 0");

            if (StockQuantity < quantity)
                throw new InvalidOperationException("Không đủ hàng trong kho");

            StockQuantity -= quantity;
        }

        // Kiểm tra xem sản phẩm có đang ở trạng thái tồn kho thấp hay không, dựa trên StockQuantity và LowStockThreshold
        public bool IsLowStock()
        {
            return StockQuantity <= LowStockThreshold;
        }

    }

}
