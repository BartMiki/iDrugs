﻿CREATE TABLE [dbo].[Prescription]
(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	DoctorId INT NOT NULL,
	ApothecaryId INT NOT NULL,
	PrescriptionDate DATE NOT NULL,
	CompletionDate DATE,
	Status VARCHAR(20) NOT NULL DEFAULT 'CREATED',
	TotalCost MONEY NOT NULL DEFAULT 0,
	Email NVARCHAR(100),
	RowVersion  INT NOT NULL DEFAULT 1,

	CHECK ( [Status] IN ('CREATED', 'IN_PROGRESS', 'COMPLETED')),
	FOREIGN KEY (DoctorId) REFERENCES Doctor(Id),
	FOREIGN KEY (ApothecaryId) REFERENCES Apothecary(Id)
)
