CREATE TABLE dbo.[Order]
(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	ApothecaryId INT NOT NULL,
	OrderCreationDate DATE DEFAULT GETDATE(),
	SendOrderDate DATE

	FOREIGN KEY (ApothecaryId) REFERENCES Apothecary(Id) 
)
