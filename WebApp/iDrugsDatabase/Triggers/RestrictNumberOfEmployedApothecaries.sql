CREATE TRIGGER RestrictNumberOfEmployedApothecaries
	ON [dbo].Apothecary
	FOR INSERT, UPDATE
	AS
	BEGIN
		DECLARE @employed INT
		SET @employed = (SELECT COUNT(*) FROM Apothecary WHERE IsEmployed = 1)

		IF @employed > 10
		BEGIN
			ROLLBACK
			RAISERROR('Osiągnięto maksymalną liczbę zatrudnionych pracowników',16,1)
		END
	END
