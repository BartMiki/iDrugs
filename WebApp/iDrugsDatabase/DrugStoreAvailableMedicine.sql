CREATE TABLE [dbo].[DrugStoreAvailableMedicine]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[DrugStoreId] INT NOT NULL,
	[MedicineId] INT NOT NULL,
	[Quantity] INT NOT NULL CHECK (Quantity >= 0),

	UNIQUE (DrugStoreId, MedicineId),

	FOREIGN KEY (DrugStoreId) REFERENCES DrugStore(Id),
	FOREIGN KEY (MedicineId) REFERENCES Medicine(Id)
)
