CREATE TABLE [dbo].[CompletedPrescription]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[PrescriptionId] INT NOT NULL,
	[ApothecaryId] INT NOT NULL,
	[CompletionDate] DATE NOT NULL DEFAULT GETDATE(),

	FOREIGN KEY (PrescriptionId) REFERENCES Prescription(Id),
	FOREIGN KEY (ApothecaryId) REFERENCES Apothecary(Id)
)
