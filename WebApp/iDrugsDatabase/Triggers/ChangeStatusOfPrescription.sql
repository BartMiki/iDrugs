CREATE TRIGGER ChangeStatusOfPrescription ON PrescriptionItem
	INSTEAD OF UPDATE
	AS
	BEGIN
		IF EXISTS (SELECT * FROM PrescriptionItem PIt
			JOIN inserted I ON I.Id = PIt.Id
			WHERE I.RowVersion != PIt.RowVersion)
		BEGIN
			RAISERROR('Błąd współbierzności, blokowanie optymistyczne', 16, 1)
		END
		ELSE IF EXISTS (SELECT * FROM PrescriptionItem PIt
			JOIN inserted I ON I.Id = PIt.Id
			WHERE PIt.Status = 'BOUGHT')
		BEGIN
			RAISERROR('Nie można zmieniać wykupionego leku na recepcie', 16, 1)
		END
		ELSE IF EXISTS (SELECT * FROM PrescriptionItem PIt
			JOIN inserted I ON I.Id = PIt.Id
			WHERE PIt.Status = 'IN_PROGRESS' AND I.Status = 'CREATED')
		BEGIN
			RAISERROR('Nie można zmieniać zaakcpetowanego leku na recepcie', 16, 1)
		END
		ELSE
		BEGIN
			UPDATE PrescriptionItem
			SET
				MedicineId = inserted.MedicineId,
				PrescriptionId = inserted.PrescriptionId,
				QuantityAlreadyBought = inserted.QuantityAlreadyBought,
				QuantityToBuy = inserted.QuantityToBuy,
				RowVersion = inserted.RowVersion + 1,
				Status = 'BOUGHT'
			FROM inserted
			WHERE PrescriptionItem.Id = inserted.Id AND inserted.QuantityAlreadyBought = inserted.QuantityToBuy

			UPDATE PrescriptionItem
			SET
				MedicineId = inserted.MedicineId,
				PrescriptionId = inserted.PrescriptionId,
				QuantityAlreadyBought = inserted.QuantityAlreadyBought,
				QuantityToBuy = inserted.QuantityToBuy,
				RowVersion = inserted.RowVersion + 1,
				Status = inserted.Status
			FROM inserted
			WHERE PrescriptionItem.Id = inserted.Id AND inserted.QuantityAlreadyBought != inserted.QuantityToBuy

			IF NOT EXISTS (SELECT * FROM PrescriptionItem PIt
				JOIN inserted I ON I.Id = PIt.Id
				WHERE PIt.Status != 'BOUGHT' AND PIt.PrescriptionId = I.PrescriptionId)
			BEGIN
				UPDATE Prescription
				SET
					Status = 'COMPLETED'
				FROM inserted I
				WHERE Prescription.Id = I.PrescriptionId
			END
		END
	END
