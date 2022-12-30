-- Example of: table variables, while cycle and if statements
CREATE PROC PrimeNumbers
	@PrintUntil INT
AS
    DECLARE @I INT=2
    DECLARE @PRIME INT=0
    DECLARE @OUTPUT TABLE (NUM INT)
    WHILE @I<=@PrintUntil
    BEGIN
        DECLARE @J INT = @I-1
        SET @PRIME=1
        WHILE @J>1
        BEGIN
            IF @I % @J=0
            BEGIN
                SET @PRIME=0
            END
            SET @J=@J-1
        END
        IF @PRIME =1
        BEGIN
            INSERT @OUTPUT VALUES (@I)
        END
        SET @I=@I+1
    END

	SELECT * FROM @OUTPUT
GO
