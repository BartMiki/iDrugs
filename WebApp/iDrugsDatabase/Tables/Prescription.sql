CREATE TABLE [dbo].[Prescription]
(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	DoctorId INT NOT NULL,
	ApothecaryId INT NOT NULL,
	PrescriptionDate DATE NOT NULL,
	TotalCost MONEY,
	RowVersion TIMESTAMP,

	FOREIGN KEY (DoctorId) REFERENCES Doctor(Id),
	FOREIGN KEY (ApothecaryId) REFERENCES Apothecary(Id)
)
