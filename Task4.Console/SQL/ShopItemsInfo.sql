CREATE PROC ShopItemsInfo
AS
    DECLARE
    @product_name VARCHAR(MAX),
    @list_price   DECIMAL;

    DECLARE cursor_product CURSOR
    FOR SELECT
            Name,
            Price
        FROM
            ShopItems;

    OPEN cursor_product;

    FETCH NEXT FROM cursor_product INTO
        @product_name,
        @list_price;

    WHILE @@FETCH_STATUS = 0
        BEGIN
            IF @product_name = '' OR @product_name IS NULL
                BEGIN
                    RAISERROR('Product name is empty or null', 16, 1)
                END
            IF @list_price <= 0.0
                BEGIN
                    RAISERROR('Product price is zero or smaller', 16, 1)
                END
            PRINT @product_name + ' ' + CAST(@list_price AS varchar);
            FETCH NEXT FROM cursor_product INTO
                @product_name,
                @list_price;
        END;

    CLOSE cursor_product;

    DEALLOCATE cursor_product;
GO