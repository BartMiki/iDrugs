CREATE TABLE dbo.OrderItem
(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	OrderId INT NOT NULL,
	MedicineId INT NOT NULL,
	Quantity INT NOT NULL CHECK (Quantity > 0)

	UNIQUE(OrderId, MedicineId)

	FOREIGN KEY (OrderId) REFERENCES [Order](Id),
	FOREIGN KEY (MedicineId) REFERENCES Medicine(Id)
)
