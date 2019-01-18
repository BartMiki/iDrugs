﻿CREATE TABLE dbo.[Order]
(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	ApothecaryId INT NOT NULL,
	OrderCreationDate DATETIME DEFAULT GETDATE(),
	SendOrderDate DATETIME,
	RowVersion TIMESTAMP,

	FOREIGN KEY (ApothecaryId) REFERENCES Apothecary(Id) 
)
