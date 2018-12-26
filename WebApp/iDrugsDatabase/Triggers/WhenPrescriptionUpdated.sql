CREATE TRIGGER [WhenPrescriptionUpdated]
	ON Prescription
	INSTEAD OF UPDATE
	AS
	BEGIN
		IF EXISTS (SELECT * FROM Prescription P
			JOIN inserted I ON I.Id = P.Id
			WHERE I.RowVersion != P.RowVersion)
		BEGIN
			RAISERROR('Błąd współbierzności, blokowanie optymistyczne', 16, 1)
		END
		ELSE IF EXISTS(SELECT * FROM Prescription P
			JOIN inserted I ON I.Id = P.Id
			WHERE P.Status = 'COMPLETED')
		BEGIN
			RAISERROR('Nie można zmieniać zakończonej recepty', 16, 1)
		END
		ELSE IF EXISTS(SELECT * FROM Prescription P
			JOIN inserted I ON I.Id = P.Id
			WHERE P.Status = 'IN_PROGRESS' AND I.Status = 'CREATED')
		BEGIN
			RAISERROR('Nie można zmieniać zaakceptowanej recepty', 16, 1)
		END
		ELSE
		BEGIN
			UPDATE Prescription
			SET
				ApothecaryId = I.ApothecaryId,
				CompletionDate = I.CompletionDate,
				DoctorId = I.DoctorId,
				Email = I.Email,
				PrescriptionDate = I.PrescriptionDate,
				RowVersion = I.RowVersion + 1,
				Status = I.Status,
				TotalCost = (SELECT SUM(M.UnitPrice * PIt.QuantityToBuy) FROM PrescriptionItem PIt
					JOIN Medicine M ON PIt.MedicineId = M.Id
					WHERE I.Id = PIt.PrescriptionId)
			FROM inserted I 
			WHERE Prescription.Id = I.Id AND I.Status != 'COMPLETED'

			UPDATE Prescription
			SET
				ApothecaryId = I.ApothecaryId,
				CompletionDate = GETDATE(),
				DoctorId = I.DoctorId,
				Email = I.Email,
				PrescriptionDate = I.PrescriptionDate,
				RowVersion = I.RowVersion + 1,
				Status = I.Status,
				TotalCost = (SELECT SUM(M.UnitPrice * PIt.QuantityToBuy) FROM PrescriptionItem PIt
					JOIN Medicine M ON PIt.MedicineId = M.Id
					WHERE I.Id = PIt.PrescriptionId)
			FROM inserted I 
			WHERE Prescription.Id = I.Id AND I.Status = 'COMPLETED'
		END
	END