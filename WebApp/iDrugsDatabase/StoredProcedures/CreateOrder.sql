CREATE PROCEDURE [dbo].[CreateOrder]
	@ApothecaryId INT
AS BEGIN
	DECLARE @OrderTable TABLE (Id INT);
	DECLARE @OrderId INT;

	INSERT INTO [Order](ApothecaryId)
	OUTPUT inserted.Id INTO @OrderTable
	VALUES (@ApothecaryId)

	SET @OrderId = (SELECT Id FROM @OrderTable)

	SELECT Id FROM @OrderTable
END