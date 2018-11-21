CREATE TABLE dbo.[Order]
(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	ApothecaryId INT NOT NULL,
	OrderDate DATE,

	FOREIGN KEY (ApothecaryId) REFERENCES Apothecary(Id) 
)
