-- Loop to insert 20 entries for each month from May 2023 to September 2024
DECLARE @DateStart DATE = '2023-05-01';
DECLARE @DateEnd DATE = '2024-09-30';
DECLARE @CurrentDate DATE;
DECLARE @OrderNumber INT = 1;
DECLARE @CustomerId INT;
DECLARE @TotalAmount DECIMAL(12, 2);

WHILE @DateStart <= @DateEnd
BEGIN
    SET @CurrentDate = @DateStart;
    
    -- Insert 20 entries for the current month
    WHILE @OrderNumber % 20 != 0
    BEGIN
        SET @CustomerId = ((@OrderNumber - 1) % 59) + 1; -- Rotate CustomerId from 1 to 50
        SET @TotalAmount = ROUND((RAND() * 100000.00), 2); -- Random total amount between 0.00 and 100000.00

        INSERT INTO [dbo].[OrderTbl] ([OrderDate], [OrderNumber], [CustomerId], [TotalAmount])
        VALUES (@CurrentDate, 'ORD' + RIGHT('000' + CAST(@OrderNumber AS VARCHAR(10)), 6), @CustomerId, @TotalAmount);

        SET @OrderNumber = @OrderNumber + 1;
        SET @CurrentDate = DATEADD(DAY, 1, @CurrentDate);
        
        -- Reset day to 1 if end of month is reached
        IF @CurrentDate > EOMONTH(@DateStart)
            SET @CurrentDate = DATEADD(DAY, 1 - DAY(@CurrentDate), @CurrentDate);
    END

    -- Move to the next month
    SET @DateStart = DATEADD(MONTH, 1, @DateStart);
    SET @OrderNumber = (@OrderNumber / 20) * 20 + 1; -- Reset the order number for the new month
END
