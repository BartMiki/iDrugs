CREATE TABLE [dbo].[Prescription]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[DoctorId] INT NOT NULL,
	[PrescriptionDate] DATE NOT NULL,
	[Value] MONEY,

	FOREIGN KEY (DoctorId) REFERENCES Doctor(Id)
)
