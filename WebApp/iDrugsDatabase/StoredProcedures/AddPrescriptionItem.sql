CREATE PROCEDURE [dbo].[AddPrescriptionItem]
	@medicineId int,
	@prescriptionId int,
	@quantityToBuy int
AS
	IF EXISTS (SELECT * FROM PrescriptionItem 
		WHERE PrescriptionId = @prescriptionId AND
		MedicineId = @medicineId)
	BEGIN
		UPDATE PrescriptionItem
		SET QuantityToBuy = QuantityToBuy + @quantityToBuy
		WHERE PrescriptionId = @prescriptionId AND
		MedicineId = @medicineId
	END
	ELSE
	BEGIN
		INSERT INTO PrescriptionItem (MedicineId, PrescriptionId, QuantityToBuy)
		VALUES (@medicineId, @prescriptionId, @quantityToBuy)
	END

	UPDATE Prescription
	SET TotalCost = TotalCost
	WHERE Id = @prescriptionId

RETURN
