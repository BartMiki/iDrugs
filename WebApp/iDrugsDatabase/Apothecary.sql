CREATE TABLE [dbo].[Apothecary]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[DrugStoreId] INT NOT NULL,
	[FirstName] NVARCHAR(50) NOT NULL,
	[LastName] NVARCHAR(50) NOT NULL,
	[MonthlySalary] MONEY NOT NULL CHECK (MonthlySalary > 0) 

	FOREIGN KEY (DrugStoreId) REFERENCES DrugStore(Id),
)
