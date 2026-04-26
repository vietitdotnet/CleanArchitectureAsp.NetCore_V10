

SET IDENTITY_INSERT Categorys ON;

INSERT INTO Categorys (Id, Name, Slug, Title, Description, ParentCategoryId, IsDeleted)
VALUES
-- Root categories
(1, N'Điện tử', 'dien-tu', N'Điện tử', N'Sản phẩm điện tử', NULL, 0),
(2, N'Thời trang', 'thoi-trang', N'Thời trang', N'Quần áo và phụ kiện', NULL, 0),
(3, N'Gia dụng', 'gia-dung', N'Gia dụng', N'Đồ dùng gia đình', NULL, 0),

-- Level 1 children
(4, N'Điện thoại', 'dien-thoai', NULL, NULL, 1, 0),
(5, N'Laptop', 'laptop', NULL, NULL, 1, 0),
(6, N'Tivi', 'tivi', NULL, NULL, 1, 0),

(7, N'Nam', 'nam', NULL, NULL, 2, 0),
(8, N'Nữ', 'nu', NULL, NULL, 2, 0),

(9, N'Nhà bếp', 'nha-bep', NULL, NULL, 3, 0),
(10, N'Phòng khách', 'phong-khach', NULL, NULL, 3, 0),

-- Level 2 children
(11, N'iPhone', 'iphone', NULL, NULL, 4, 0),
(12, N'Samsung', 'samsung', NULL, NULL, 4, 0),

(13, N'Gaming Laptop', 'gaming-laptop', NULL, NULL, 5, 0),
(14, N'Ultrabook', 'ultrabook', NULL, NULL, 5, 0),

(15, N'Tivi 4K', 'tivi-4k', NULL, NULL, 6, 0),
(16, N'Tivi OLED', 'tivi-oled', NULL, NULL, 6, 0),

(17, N'Áo nam', 'ao-nam', NULL, NULL, 7, 0),
(18, N'Quần nam', 'quan-nam', NULL, NULL, 7, 0),

(19, N'Áo nữ', 'ao-nu', NULL, NULL, 8, 0),
(20, N'Váy', 'vay', NULL, NULL, 8, 0),

(21, N'Nồi', 'noi', NULL, NULL, 9, 0),
(22, N'Chảo', 'chao', NULL, NULL, 9, 0),

(23, N'Sofa', 'sofa', NULL, NULL, 10, 0),
(24, N'Bàn trà', 'ban-tra', NULL, NULL, 10, 0),

-- Level 3 children (deep tree)
(25, N'iPhone 13', 'iphone-13', NULL, NULL, 11, 0),
(26, N'iPhone 14', 'iphone-14', NULL, NULL, 11, 0),

(27, N'Samsung S23', 'samsung-s23', NULL, NULL, 12, 0),
(28, N'Samsung A Series', 'samsung-a', NULL, NULL, 12, 0),

(29, N'Laptop RTX', 'laptop-rtx', NULL, NULL, 13, 0),
(30, N'Laptop GTX', 'laptop-gtx', NULL, NULL, 13, 0),

(31, N'Ultrabook Dell', 'ultrabook-dell', NULL, NULL, 14, 0),
(32, N'Ultrabook HP', 'ultrabook-hp', NULL, NULL, 14, 0),

(33, N'Tivi Sony 4K', 'sony-4k', NULL, NULL, 15, 0),
(34, N'Tivi LG 4K', 'lg-4k', NULL, NULL, 15, 0),

(35, N'Tivi OLED Sony', 'oled-sony', NULL, NULL, 16, 0),
(36, N'Tivi OLED LG', 'oled-lg', NULL, NULL, 16, 0),

(37, N'Áo thun nam', 'ao-thun-nam', NULL, NULL, 17, 0),
(38, N'Áo sơ mi nam', 'ao-so-mi-nam', NULL, NULL, 17, 0),

(39, N'Quần jean nam', 'quan-jean-nam', NULL, NULL, 18, 0),
(40, N'Quần tây nam', 'quan-tay-nam', NULL, NULL, 18, 0),

(41, N'Áo nữ công sở', 'ao-nu-cong-so', NULL, NULL, 19, 0),
(42, N'Áo nữ dạo phố', 'ao-nu-dao-pho', NULL, NULL, 19, 0),

(43, N'Váy ngắn', 'vay-ngan', NULL, NULL, 20, 0),
(44, N'Váy dài', 'vay-dai', NULL, NULL, 20, 0),

(45, N'Nồi inox', 'noi-inox', NULL, NULL, 21, 0),
(46, N'Nồi chống dính', 'noi-chong-dinh', NULL, NULL, 21, 0),

(47, N'Chảo lớn', 'chao-lon', NULL, NULL, 22, 0),
(48, N'Chảo nhỏ', 'chao-nho', NULL, NULL, 22, 0),

(49, N'Sofa da', 'sofa-da', NULL, NULL, 23, 0),
(50, N'Sofa vải', 'sofa-vai', NULL, NULL, 23, 0);

SET IDENTITY_INSERT Categorys OFF;



SET IDENTITY_INSERT Countries ON;

INSERT INTO Countries (Id, Code, Name, FlagIcon)
VALUES
(1, 'VN', N'Việt Nam', 'vn.png'),
(2, 'US', N'Hoa Kỳ', 'us.png'),
(3, 'JP', N'Nhật Bản', 'jp.png'),
(4, 'KR', N'Hàn Quốc', 'kr.png'),
(5, 'CN', N'Trung Quốc', 'cn.png'),
(6, 'DE', N'Đức', 'de.png'),
(7, 'FR', N'Pháp', 'fr.png'),
(8, 'IT', N'Ý', 'it.png'),
(9, 'GB', N'Anh', 'gb.png'),
(10, 'CA', N'Canada', 'ca.png');

SET IDENTITY_INSERT Countries OFF;


SET IDENTITY_INSERT Manufacturers ON;

INSERT INTO Manufacturers (Id, Name, ShortDescription, Website, CountryId)
VALUES
-- Việt Nam
(1, N'VinFast', N'Hãng xe điện Việt Nam', 'https://vinfastauto.com', 1),
(2, N'BKAV', N'Công nghệ & bảo mật', 'https://bkav.com.vn', 1),

-- Hoa Kỳ
(3, N'Apple', N'Công nghệ cao cấp', 'https://apple.com', 2),
(4, N'Tesla', N'Xe điện', 'https://tesla.com', 2),

-- Nhật Bản
(5, N'Sony', N'Điện tử', 'https://sony.com', 3),
(6, N'Toyota', N'Ô tô', 'https://toyota.com', 3),

-- Hàn Quốc
(7, N'Samsung', N'Điện tử', 'https://samsung.com', 4),
(8, N'LG', N'Điện tử gia dụng', 'https://lg.com', 4),

-- Trung Quốc
(9, N'Huawei', N'Viễn thông', 'https://huawei.com', 5),
(10, N'Xiaomi', N'Điện tử giá tốt', 'https://mi.com', 5),

-- Đức
(11, N'BMW', N'Ô tô cao cấp', 'https://bmw.com', 6),
(12, N'Bosch', N'Công nghệ & công nghiệp', 'https://bosch.com', 6),

-- Pháp
(13, N'Oreal', N'Mỹ phẩm', 'https://loreal.com', 7),
(14, N'Renault', N'Ô tô', 'https://renault.com', 7),

-- Ý
(15, N'Ferrari', N'Siêu xe', 'https://ferrari.com', 8),
(16, N'Armani', N'Thời trang', 'https://armani.com', 8),

-- Anh
(17, N'Rolls-Royce', N'Ô tô siêu sang', 'https://rolls-roycemotorcars.com', 9),
(18, N'Unilever', N'Hàng tiêu dùng', 'https://unilever.com', 9),

-- Canada
(19, N'Shopify', N'Nền tảng thương mại điện tử', 'https://shopify.com', 10),
(20, N'BlackBerry', N'Công nghệ bảo mật', 'https://blackberry.com', 10);

SET IDENTITY_INSERT Manufacturers OFF;

SET IDENTITY_INSERT Taxes ON;

INSERT INTO Taxes (Id, Name, Percentage)
VALUES
(1, N'VAT 0%', 0),
(2, N'VAT 5%', 5),
(3, N'VAT 8%', 8),
(4, N'VAT 10%', 10),

(5, N'Thuế tiêu thụ đặc biệt 15%', 15),
(6, N'Thuế tiêu thụ đặc biệt 20%', 20),
(7, N'Thuế tiêu thụ đặc biệt 30%', 30),

(8, N'Thuế nhập khẩu 5%', 5),
(9, N'Thuế nhập khẩu 10%', 10),
(10, N'Thuế nhập khẩu 15%', 15),
(11, N'Thuế nhập khẩu 25%', 25),

(12, N'Thuế môi trường 3%', 3),
(13, N'Thuế môi trường 5%', 5),

(14, N'Thuế dịch vụ 2%', 2),
(15, N'Thuế dịch vụ 7%', 7),

(16, N'Thuế bán hàng 6%', 6),
(17, N'Thuế bán hàng 9%', 9),

(18, N'Thuế đặc biệt 12%', 12),
(19, N'Thuế đặc biệt 18%', 18),
(20, N'Thuế đặc biệt 22%', 22);

SET IDENTITY_INSERT Taxes OFF;
/*
DECLARE @i INT = 1;

WHILE @i <= 100
BEGIN
    INSERT INTO Products
    (
        Sku, Barcode, Slug, Name, BrandName,
        CostPrice, BasePrice, Status,
        ShortDescription, Description,
        PackingSize, RegistrationNumber, DosageForm, Ingredient,
        StockQuantity, LowStockThreshold,
        DateCreate,
        CategoryId, ManufacturerId, TaxId
    )
    VALUES
    (
        CONCAT('SKU', FORMAT(@i, '0000')),
        CONCAT('BAR', FORMAT(@i, '000000')),
        CONCAT('san-pham-', @i),
        CONCAT(N'Sản phẩm ', @i),
        CONCAT(N'Brand ', ((@i - 1) % 20) + 1),

        -- Giá
        10000 + (@i * 500),
        15000 + (@i * 700),

        1, -- Active

        CONCAT(N'Mô tả ngắn sản phẩm ', @i),
        CONCAT(N'Mô tả chi tiết sản phẩm ', @i),

        CONCAT(@i % 10 + 1, N' viên/hộp'),
        CONCAT('REG', FORMAT(@i, '0000')),
        N'Viên nén',
        N'Paracetamol',

        -- Stock
        (@i * 3) % 200,
        10,

        GETUTCDATE(),

        -- FK
        ((@i - 1) % 50) + 1,   -- CategoryId (1–50)
        ((@i - 1) % 20) + 1,   -- ManufacturerId (1–20)
        ((@i - 1) % 20) + 1    -- TaxId (1–20)
    );

    SET @i = @i + 1;
END
*/

/*
DECLARE @i INT = 1;

WHILE @i <= 100
BEGIN
    -- ===== Base Unit =====
    INSERT INTO ProductUnits
    (
        ProductId,
        Barcode,
        UnitName,
        ConversionRate,
        SellingPrice,
        IsBaseUnit
    )
    VALUES
    (
        @i,
        CONCAT('PU-BAR-', FORMAT(@i, '0000'), '-1'),
        N'Viên',
        1,
        15000 + (@i * 700), -- giá lẻ
        1
    );

    -- ===== Box Unit =====
    INSERT INTO ProductUnits
    (
        ProductId,
        Barcode,
        UnitName,
        ConversionRate,
        SellingPrice,
        IsBaseUnit
    )
    VALUES
    (
        @i,
        CONCAT('PU-BAR-', FORMAT(@i, '0000'), '-2'),
        N'Hộp',
        10, -- 1 hộp = 10 viên
        (15000 + (@i * 700)) * 10 * 0.95, -- giảm nhẹ khi mua hộp
        0
    );

    SET @i = @i + 1;
END
*/

/*
DECLARE @i INT = 1;

WHILE @i <= 50
BEGIN
    INSERT INTO Medicines
    (
        ProductId,
        Dosage,
        Contraindications,
        MedicineType
    )
    VALUES
    (
        @i, -- gán vào Product 1 → 50

        -- Dosage
        CASE (@i % 3)
            WHEN 0 THEN N'Uống 1 viên mỗi 8 giờ'
            WHEN 1 THEN N'Uống 2 viên mỗi ngày sau ăn'
            ELSE N'Uống 1 viên mỗi ngày'
        END,

        -- Contraindications
        CASE (@i % 4)
            WHEN 0 THEN N'Không dùng cho phụ nữ mang thai'
            WHEN 1 THEN N'Không dùng cho trẻ dưới 6 tuổi'
            WHEN 2 THEN N'Dị ứng với thành phần thuốc'
            ELSE N'Không dùng khi có bệnh gan nặng'
        END,

        -- MedicineType random
        ABS(CHECKSUM(NEWID())) % 2  -- 0 hoặc 1
    );

    SET @i = @i + 1;
END
*/
SET IDENTITY_INSERT Promotions ON;

INSERT INTO Promotions
(Id, Name, Value, IsPercentage, MaxDiscountAmount, StartDate, EndDate, IsActive)
VALUES
(1, N'Giảm 10% toàn bộ', 10, 1, 50000, GETDATE(), DATEADD(DAY, 30, GETDATE()), 1),
(2, N'Giảm 5% nhẹ', 5, 1, 30000, GETDATE(), DATEADD(DAY, 15, GETDATE()), 1),
(3, N'Giảm 20k', 20000, 0, NULL, GETDATE(), DATEADD(DAY, 10, GETDATE()), 1),
(4, N'Flash sale 15%', 15, 1, 70000, GETDATE(), DATEADD(DAY, 5, GETDATE()), 1),
(5, N'Xả kho giảm 50k', 50000, 0, NULL, GETDATE(), DATEADD(DAY, 7, GETDATE()), 1);

SET IDENTITY_INSERT Promotions OFF;

INSERT INTO PromotionItems
(
    PromotionId,
    ProductUnitId,
    ProductName,
    UnitName,
    UnitBarcode,
    OriginalPrice,
    OverrideValue,
    IsPercentageOverride,
    IsActive
)
SELECT
    ((ROW_NUMBER() OVER (ORDER BY pu.Id) - 1) % 5) + 1,
    pu.Id,
    p.Name,
    pu.UnitName,
    pu.Barcode,
    pu.SellingPrice,

    CASE 
        WHEN ROW_NUMBER() OVER (ORDER BY pu.Id) % 4 = 0 THEN 5
        WHEN ROW_NUMBER() OVER (ORDER BY pu.Id) % 5 = 0 THEN 10000
        ELSE NULL
    END,

    CASE 
        WHEN ROW_NUMBER() OVER (ORDER BY pu.Id) % 4 = 0 THEN 1
        WHEN ROW_NUMBER() OVER (ORDER BY pu.Id) % 5 = 0 THEN 0
        ELSE NULL
    END,

    CASE 
        WHEN ROW_NUMBER() OVER (ORDER BY pu.Id) % 7 = 0 THEN 0
        ELSE 1
    END

FROM ProductUnits pu
JOIN Products p ON p.Id = pu.ProductId
WHERE pu.Id <= 50;
