CREATE TRIGGER [UpdateOnlyEmployedApothecary]
	ON [dbo].Apothecary
	FOR UPDATE
	AS
	BEGIN
		IF EXISTS (SELECT * FROM Apothecary A JOIN inserted I ON A.Id = I.Id WHERE A.IsEmployed = 0)
		BEGIN
			ROLLBACK
			RAISERROR('Nie można zmieniać danych zwolnionych pracowników',16,1)
		END
	END
