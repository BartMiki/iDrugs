CREATE PROCEDURE [dbo].[AddPrescription]
	@doctorId int,
	@apothecaryId int,
	@prescriptionDate Date,
	@email nvarchar(100)
AS BEGIN
	DECLARE @PrescriptionTable TABLE (Id INT);

	INSERT INTO Prescription (ApothecaryId, DoctorId, PrescriptionDate, Email)
	OUTPUT inserted.Id INTO @PrescriptionTable
	VALUES (@apothecaryId, @doctorId, @prescriptionDate, @email)

	SELECT Id FROM @PrescriptionTable
END
