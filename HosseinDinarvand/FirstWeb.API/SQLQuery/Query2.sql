CREATE PROCEDURE UpdateProduct
	@ProductId INT,
	@ProductName NVARCHAR(100),
	@Category NVARCHAR(50),
	@Price DECIMAL(10,2)
AS
BEGIN
	UPDATE Products
	SET [Name] = @ProductName,
		Category = @Category,
		Price = @Price
	WHERE Id = @ProductId
END