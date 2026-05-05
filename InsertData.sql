SET NOCOUNT ON;

-------------------------------------------------
-- CATEGORY (50)
-------------------------------------------------
DECLARE @i INT = 1;

WHILE @i <= 50
BEGIN
    INSERT INTO dbo.Categorys
    (
        Name,
        Slug,
        Title,
        Description,
        ParentCategoryId,
        IsDeleted
    )
    VALUES
    (
        CONCAT('Category ', @i),
        CONCAT('category-', @i),
        CONCAT('Title Category ', @i),
        CONCAT('Description for category ', @i),
        CASE 
            WHEN @i <= 10 THEN NULL
            ELSE ((@i - 1) % 10) + 1
        END,
        0
    );

    SET @i = @i + 1;
END

-------------------------------------------------
-- PRODUCT (50)
-------------------------------------------------
SET @i = 1;

WHILE @i <= 50
BEGIN
    INSERT INTO Products
    (
        Name,
        Slug,
        Description,
        Price,
        DateCreate,
        DateUpdate,
        CategoryId
    )
    VALUES
    (
        CONCAT('Product ', @i),
        CONCAT('product-', @i),
        CONCAT('Description for product ', @i),
        ROUND(RAND() * 1000 + 10, 2),
        GETDATE(),
        NULL,
        ((@i - 1) % 50) + 1
    );

    SET @i = @i + 1;
END

-------------------------------------------------
-- ORDERS (50)
-------------------------------------------------
SET @i = 1;

WHILE @i <= 50
BEGIN
    INSERT INTO Orders
    (
        Description,
        OrderDate,
        Status,
        CreatedByUserId
    )
    VALUES
    (
        CONCAT('Order description ', @i),
        DATEADD(DAY, -@i, GETDATE()),
        ABS(CHECKSUM(NEWID())) % 3,
        '3e317e40-7545-4933-8d08-2e300196381f'
    );

    SET @i = @i + 1;
END

-------------------------------------------------
-- ORDER PRODUCT (50)
-------------------------------------------------
DECLARE @k INT; -- Thêm dòng này để định nghĩa biến
SET @k = 1;

WHILE @k <= 50
BEGIN
    INSERT INTO OrderProducts
    (
        Id,
        OrderId,
        ProductId,
        Quantity,
        Price
    )
    VALUES
    (
        @k,        -- Dùng biến @i làm giá trị Id
        ABS(CHECKSUM(NEWID())) % 50 + 1,
        ABS(CHECKSUM(NEWID())) % 50 + 1,
        ABS(CHECKSUM(NEWID())) % 5 + 1,
        ROUND(RAND() * 500 + 20, 2)
    );

    SET @k = @k + 1;
END

INSERT INTO Brands (Name, NormalizedName, Code, Description, Website, Country, LogoUrl, IsActive)
VALUES
(N'Brauer', 'BRAUER', 'BRAUER', N'Thương hiệu Úc chuyên DHA và vitamin', 'https://brauer.com.au', 'AU', NULL, 1),
(N'Blackmores', 'BLACKMORES', 'BLACK', N'Thực phẩm chức năng nổi tiếng Úc', 'https://blackmores.com.au', 'AU', NULL, 1),
(N'Doppelherz', 'DOPPELHERZ', 'DOPPEL', N'Thương hiệu Đức', 'https://doppelherz.de', 'DE', NULL, 1),
(N'Kirkland', 'KIRKLAND', 'KIRK', N'Thương hiệu Mỹ của Costco', 'https://costco.com', 'US', NULL, 1),
(N'Nature Made', 'NATURE MADE', 'NM', N'Vitamin Mỹ', 'https://naturemade.com', 'US', NULL, 1),
(N'Centrum', 'CENTRUM', 'CENT', N'Vitamin tổng hợp', 'https://centrum.com', 'US', NULL, 1),
(N'Wellbaby', 'WELLBABY', 'WB', N'Sản phẩm cho trẻ em', 'https://vitabiotics.com', 'UK', NULL, 1),
(N'Vitabiotics', 'VITABIOTICS', 'VITA', N'Thương hiệu Anh', 'https://vitabiotics.com', 'UK', NULL, 1),
(N'Healthy Care', 'HEALTHY CARE', 'HC', N'Thương hiệu Úc', 'https://healthycare.com.au', 'AU', NULL, 1),
(N'Mega We Care', 'MEGA WE CARE', 'MEGA', N'Thương hiệu Thái Lan', 'https://megawecare.com', 'TH', NULL, 1),

(N'Nature’s Way', 'NATURE’S WAY', 'NW', N'Vitamin trẻ em', 'https://naturesway.com', 'US', NULL, 1),
(N'Puritan’s Pride', 'PURITAN’S PRIDE', 'PP', N'Thực phẩm chức năng Mỹ', 'https://puritanspride.com', 'US', NULL, 1),
(N'Now Foods', 'NOW FOODS', 'NOW', N'Thực phẩm bổ sung', 'https://nowfoods.com', 'US', NULL, 1),
(N'Swisse', 'SWISSE', 'SWISSE', N'Thương hiệu Úc', 'https://swisse.com', 'AU', NULL, 1),
(N'Bio Island', 'BIO ISLAND', 'BIO', N'Sản phẩm cho bé', 'https://bioisland.com.au', 'AU', NULL, 1),
(N'ChildLife', 'CHILDLIFE', 'CL', N'Vitamin trẻ em', 'https://childlife.com', 'US', NULL, 1),
(N'Ostelin', 'OSTELIN', 'OST', N'Vitamin D Úc', 'https://ostelin.com.au', 'AU', NULL, 1),
(N'Elevit', 'ELEVIT', 'ELE', N'Vitamin cho mẹ bầu', 'https://elevit.com', 'DE', NULL, 1),
(N'Ensure', 'ENSURE', 'ENS', N'Sữa dinh dưỡng', 'https://ensure.com', 'US', NULL, 1),
(N'Abbott', 'ABBOTT', 'ABB', N'Tập đoàn y tế', 'https://abbott.com', 'US', NULL, 1),

(N'Nestle', 'NESTLE', 'NES', N'Thực phẩm toàn cầu', 'https://nestle.com', 'CH', NULL, 1),
(N'Danone', 'DANONE', 'DAN', N'Sữa và thực phẩm', 'https://danone.com', 'FR', NULL, 1),
(N'Meiji', 'MEIJI', 'MEI', N'Sữa Nhật Bản', 'https://meiji.co.jp', 'JP', NULL, 1),
(N'Morinaga', 'MORINAGA', 'MORI', N'Sữa Nhật', 'https://morinaga.co.jp', 'JP', NULL, 1),
(N'PediaSure', 'PEDIASURE', 'PED', N'Dinh dưỡng trẻ em', 'https://pediasure.com', 'US', NULL, 1),
(N'Enfa', 'ENFA', 'ENFA', N'Sữa Mead Johnson', 'https://enfamil.com', 'US', NULL, 1),
(N'Aptamil', 'APTAMIL', 'APT', N'Sữa Đức', 'https://aptamil.com', 'DE', NULL, 1),
(N'Friso', 'FRISO', 'FRI', N'Sữa Hà Lan', 'https://friso.com', 'NL', NULL, 1),
(N'Dumex', 'DUMEX', 'DUM', N'Sữa trẻ em', 'https://dumex.com', 'FR', NULL, 1),
(N'Colosbaby', 'COLOSBABY', 'COLO', N'Sữa non', 'https://colosbaby.vn', 'VN', NULL, 1),

(N'Vinamilk', 'VINAMILK', 'VNM', N'Sữa Việt Nam', 'https://vinamilk.com.vn', 'VN', NULL, 1),
(N'True Milk', 'TRUE MILK', 'TH', N'Sữa TH', 'https://thmilk.vn', 'VN', NULL, 1),
(N'Nutifood', 'NUTIFOOD', 'NTF', N'Dinh dưỡng VN', 'https://nutifood.com.vn', 'VN', NULL, 1),
(N'Dược Hậu Giang', 'DƯỢC HẬU GIANG', 'DHG', N'Dược phẩm VN', 'https://dhgpharma.com.vn', 'VN', NULL, 1),
(N'Imexpharm', 'IMEXPHARM', 'IMP', N'Dược phẩm VN', 'https://imexpharm.com', 'VN', NULL, 1),
(N'Trường Thọ', 'TRƯỜNG THỌ', 'TT', N'Sữa VN', 'https://truongtho.com.vn', 'VN', NULL, 1),
(N'Sanofi', 'SANOFI', 'SAN', N'Dược phẩm Pháp', 'https://sanofi.com', 'FR', NULL, 1),
(N'Pfizer', 'PFIZER', 'PFZ', N'Dược phẩm Mỹ', 'https://pfizer.com', 'US', NULL, 1),
(N'Bayer', 'BAYER', 'BAYER', N'Dược phẩm Đức', 'https://bayer.com', 'DE', NULL, 1),
(N'GSK', 'GSK', 'GSK', N'Dược phẩm Anh', 'https://gsk.com', 'UK', NULL, 1),

(N'Unilever', 'UNILEVER', 'UNI', N'Hàng tiêu dùng', 'https://unilever.com', 'UK', NULL, 1),
(N'P&G', 'P&G', 'PG', N'Hàng tiêu dùng Mỹ', 'https://pg.com', 'US', NULL, 1),
(N'L’Oreal', 'L’OREAL', 'LOREAL', N'Mỹ phẩm Pháp', 'https://loreal.com', 'FR', NULL, 1),
(N'Shiseido', 'SHISEIDO', 'SHI', N'Mỹ phẩm Nhật', 'https://shiseido.com', 'JP', NULL, 1),
(N'Innisfree', 'INNISFREE', 'INNI', N'Mỹ phẩm Hàn', 'https://innisfree.com', 'KR', NULL, 1),
(N'Laneige', 'LANEIGE', 'LAN', N'Mỹ phẩm Hàn', 'https://laneige.com', 'KR', NULL, 1),
(N'The Face Shop', 'THE FACE SHOP', 'TFS', N'Mỹ phẩm Hàn', 'https://thefaceshop.com', 'KR', NULL, 1),
(N'Sulwhasoo', 'SULWHASOO', 'SUL', N'Mỹ phẩm cao cấp Hàn', 'https://sulwhasoo.com', 'KR', NULL, 1),
(N'Hada Labo', 'HADA LABO', 'HADA', N'Mỹ phẩm Nhật', 'https://hadalabo.com', 'JP', NULL, 1),
(N'Rohto', 'ROHTO', 'ROHTO', N'Dược mỹ phẩm Nhật', 'https://rohto.com', 'JP', NULL, 1);