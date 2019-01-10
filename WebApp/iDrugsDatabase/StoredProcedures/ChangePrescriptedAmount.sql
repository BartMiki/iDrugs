CREATE PROCEDURE [dbo].[ChangePrescriptedAmount]
	@id int = 0,
	@newAmount int,
	@version int
AS
BEGIN
	UPDATE PrescriptionItem
	SET QuantityToBuy = @newAmount,
	RowVersion = @version
	WHERE Id = @id

END
