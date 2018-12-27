CREATE PROCEDURE [dbo].[AcceptCreatedPrescription]
	@id int
AS
	IF EXISTS (SELECT * FROM Prescription WHERE Id = @id AND [Status] != 'CREATED')
	BEGIN
		RAISERROR('Nie można zaakcpetować tworzonej recepty, ponieważ jest już stworzona lub zakończona', 16, 1)
		RETURN 1
	END
	ELSE IF NOT EXISTS (SELECT * FROM PrescriptionItem WHERE PrescriptionId = @id)
	BEGIN
		RAISERROR('Nie można zaakcpetować recepty bez leków', 16, 1)
		RETURN 1
	END
	ELSE
	BEGIN
		UPDATE Prescription
		SET Status = 'IN_PROGRESS'
		WHERE Id = @id

		UPDATE PrescriptionItem
		SET STATUS = 'IN_PROGRESS'
		WHERE PrescriptionId = @id
	END
RETURN 0
