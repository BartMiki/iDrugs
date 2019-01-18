CREATE PROCEDURE [dbo].[RemoveDoctorLicense]
	@id int
AS
	IF EXISTS (SELECT * FROM Doctor WHERE Id = @id AND HasValidMedicalLicense = 0)
	BEGIN
		RAISERROR('Nie można ponownie odebrać licencji lekarzowi',16,1)
		RETURN
	END
	UPDATE Doctor
	SET HasValidMedicalLicense = 0
	WHERE Id = @id
RETURN 0
