CREATE TABLE [dbo].[RejectedPrescription]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[PrescriptionId] INT NOT NULL,
	[ApothecaryId] INT NOT NULL,
	[RejectionReason] NVARCHAR(200),
	[RejectionDate] DATE DEFAULT GETDATE(),

	FOREIGN KEY (PrescriptionId) REFERENCES Prescription(Id),
	FOREIGN KEY (ApothecaryId) REFERENCES Apothecary(Id),
)
