CREATE PROCEDURE [dbo].[FireApothecary]
	@id int
AS
	IF EXISTS (SELECT * FROM Apothecary WHERE Id = @id AND IsEmployed = 0)
	BEGIN
		RAISERROR('Nie można zwolnić pracownika, który jest już zwolniony',16,1)
		RETURN
	END
	UPDATE Apothecary
	SET IsEmployed = 0
	WHERE Id = @id
RETURN 0
