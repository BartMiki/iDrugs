CREATE TABLE [dbo].[PrescriptionItem]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[PrescriptionId] INT NOT NULL,
	[QuantityToBuy] INT NOT NULL CHECK ([QuantityToBuy] > 0),
	[Status] VARCHAR(20) NOT NULL DEFAULT 'CREATED' CHECK (Status IN ('CREATED', 'IN_PROGRESS', 'BOUGHT')),
	[QuantityAlreadyBought] INT NOT NULL DEFAULT 0 CHECK ([QuantityToBuy] >= [QuantityAlreadyBought]),
	[MedicineId] INT NOT NULL,
	RowVersion INT NOT NULL DEFAULT 1,

	FOREIGN KEY (MedicineId) REFERENCES Medicine(Id),
	FOREIGN KEY (PrescriptionId) REFERENCES Prescription(Id),

)
