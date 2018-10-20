CREATE TABLE [dbo].[Medicine]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[UnitPrice] MONEY NOT NULL, 
    [Amount] INT NOT NULL, 
    [MedicineTypeId] INT NOT NULL,
	[Refund] DECIMAL(3,2),
	[Name] NVARCHAR(50) NOT NULL,

	FOREIGN KEY (MedicineTypeId) REFERENCES MedicineType(Id) 

)
