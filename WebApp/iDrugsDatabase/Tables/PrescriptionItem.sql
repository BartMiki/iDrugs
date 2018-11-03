CREATE TABLE [dbo].[PrescriptionItem]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[PrescriptionId] INT NOT NULL,
	[Quantity] INT NOT NULL CHECK (Quantity > 0),
	[MedicineId] INT NOT NULL,

	FOREIGN KEY (MedicineId) REFERENCES Medicine(Id),
	FOREIGN KEY (PrescriptionId) REFERENCES Prescription(Id),

)
