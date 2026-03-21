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
