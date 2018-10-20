CREATE TABLE [dbo].[ApothecaryShift]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ApothecaryId] INT NOT NULL,
	[ShiftStart] DATETIME NOT NULL DEFAULT GETDATE(),
	[ShiftEnd] DATETIME,
	[TotalTime] TIME,
	[ServedClientsCount] INT,

	FOREIGN KEY (ApothecaryId) REFERENCES Apothecary(Id),
)
