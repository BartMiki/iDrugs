CREATE TABLE dbo.MedicineWarehouse
(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	MedicineId INT NOT NULL,
	Quantity INT NOT NULL CHECK (Quantity >= 0),
	RowVersion TIMESTAMP,

	UNIQUE (MedicineId),

	FOREIGN KEY (MedicineId) REFERENCES Medicine(Id)

)
