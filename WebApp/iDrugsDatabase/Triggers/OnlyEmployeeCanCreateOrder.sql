CREATE TRIGGER [Trigger1]
	ON [dbo].[Order]
	FOR INSERT
	AS
	BEGIN
		IF EXISTS (SELECT * FROM inserted i 
			JOIN Apothecary a on i.ApothecaryId = a.Id
			WHERE a.IsEmployed = 0)
		BEGIN
			ROLLBACK
			RAISERROR('Zwolniony aptekarz nie może stworzyć zamówienia',16,1)
		END
	END
