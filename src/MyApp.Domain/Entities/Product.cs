using MyApp.Domain.Core.Models;
using MyApp.Domain.Enums;
using MyApp.Domain.Extentions;

namespace MyApp.Domain.Entities
{
    public class Product : BaseEntity<int>
    {      
        private Product() { }
        public Guid PublicId { get; private set; }
        public string Sku { get; private set; } = null!; // mã sản phẩm
        public string? Barcode { get; private set; } // mã vạch, nếu có
        public string Slug { get; private set; } = null!; // slug dùng để tạo URL thân thiện, có thể dùng để hiển thị trong danh sách sản phẩm
        public string ShortName { get; private set; } = null!; // tên ngắn gọn của sản phẩm, có thể dùng để hiển thị trong danh sách sản phẩm hoặc trên giao diện bán hàng khi cần hiển thị tên ngắn hơn
        public string Name { get; private set; } = null!; // tên đầy đủ của sản phẩm, dùng để hiển thị chi tiết sản phẩm và trong các báo cáo
        public decimal CostPrice { get; private set; }  // giá nhập (giá vốn) - giá mà nhà thuốc mua vào    
        public decimal BasePrice { get; private set; }  //giá gốc (giá niêm yết)       
        public ProductStatus Status { get; private set; } // trạng thái sản phẩm (Active, Inactive, Discontinued)

        public string PackingSize { get; private set; } = null!; // quy cách đóng gói
        public string? BrandName { get; private set; } // tên thương hiệu;
        public string? ShortDescription { get; private set; }// mô tả ngắn gọn về sản phẩm, có thể dùng để hiển thị trong danh sách sản phẩm
        public string? Description { get; private set; }// mô tả chi tiết về sản phẩm

        public string? RegistrationNumber { get; private set; }// số đăng ký, nếu có
        public string? DosageForm { get; private set; } // dạng bào chế 
        public string? Ingredient { get; private set; } // thành phần chính của sản phẩm, có thể dùng để hiển thị trong chi tiết sản phẩm

        public string? Benefit { get; private set; } // công dụng chính của sản phẩm, có thể dùng để hiển thị trong chi tiết sản phẩm

        public DateTimeOffset DateCreate { get; private set; }
        public DateTimeOffset? DateUpdate { get; private set; }

        public int? CategoryId { get; private set; }
        public Category? Category { get; private set; }

        public int? ManufacturerId { get; private set; } // nhà sản xuất chính của sản phẩm, nếu có
        public Manufacturer? Manufacturer { get; private set; }// thông tin chuyên biệt cho nhà sản xuất, nếu có

        public int? TaxId { get; private set; } // thuế VAT áp dụng cho sản phẩm, nếu có
        public Tax Tax { get; private set; } = null!; // thông tin chuyên biệt cho thuế, nếu có

        public Medicine? Medicine { get; private set; } // thông tin chuyên biệt cho sản phẩm là thuốc, nếu có


        private readonly List<ProductUnit> _productUnits = new();
        public IReadOnlyCollection<ProductUnit> ProductUnits => _productUnits.AsReadOnly();

        private Product(
            string sku,    
            string slug, 
            string name, 
            decimal costPrice, 
            decimal basePrice,
            string packingSize,
            string? benefit,
            string? brandName,
            string? barcode, 
            string? shortDescription, 
            string? description,
            string? registrationNumber, 
            string? dosageForm, 
            string? ingredient,
            int? categoryId,
            int? manufacturerId,
            int? taxId

            )
        {
            Sku = sku;
            Barcode = barcode;
            Slug = slug;
            Name = name;
            CostPrice = costPrice;
            BasePrice = basePrice;
            PackingSize = packingSize;
            BrandName = brandName;
            Status = ProductStatus.Pending;
            ShortDescription = shortDescription;
            Description = description;        
            RegistrationNumber = registrationNumber;
            DosageForm = dosageForm;
            Ingredient = ingredient;
            CategoryId = categoryId;
            ManufacturerId = manufacturerId;
            TaxId = taxId;
            Benefit = benefit;

        }

        public static Product Create(
            string slug,
            string name,     
            decimal costPrice,
            decimal basePrice,
            string packingSize,
            string? benefit = null,
            string? brandName = null,
            string? sku = null,
            string? barcode = null,
            string? shortDescription = null,
            string? description = null, 
            string? registrationNumber = null,
            string? dosageForm = null,
            string? ingredient = null,
            int? categoryId = null,
            int? manufacturerId = null,
            int? taxId = null
            )
        {
            // 1. Validation (Nên giữ lại như bạn đã viết)
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.");
            if(string.IsNullOrWhiteSpace(packingSize))
                throw new ArgumentException("Packing size is required.");
            if (costPrice < 0)
                throw new ArgumentException("Cost price cannot be negative.");
            if (basePrice < 0)
                throw new ArgumentException("Base price cannot be negative.");

            // 2. Xử lý logic SKU trước khi gọi Constructor
            var finalSku = string.IsNullOrWhiteSpace(sku)
                ? GenerateUnique.GenerateUniqueSku("PRD")
                : sku.Trim().ToUpper();

            // 3. Khởi tạo đối tượng
            return new Product(
                finalSku, // Truyền biến đã xử lý
                slug.Trim().ToLowerInvariant(),
                name.Trim(),
                costPrice,
                basePrice,
                packingSize.Trim(),
                benefit?.Trim(),
                brandName?.Trim(),
                barcode?.Trim(),
                shortDescription?.Trim(),
                description?.Trim(),
                registrationNumber?.Trim(),
                dosageForm?.Trim(),
                ingredient?.Trim(),
                categoryId,
                manufacturerId,
                taxId
            );
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

            _productUnits.Add(unit);

            return unit;
        }

        public void RemoveUnit(string unitName)
        {
            var unit = ProductUnits.FirstOrDefault(x => x.UnitName == unitName);

            if (unit == null)
                throw new InvalidOperationException("Unit not found");

            _productUnits.Remove(unit);
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

    }

}
