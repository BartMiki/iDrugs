CREATE TABLE [dbo].[Medicine]
(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	UnitPrice MONEY NOT NULL, 
    Amount INT NOT NULL, 
    MedicineTypeId INT NOT NULL,
	Refund DECIMAL(3,2),
	Name NVARCHAR(50) NOT NULL,
	Expired BIT NOT NULL DEFAULT(1)

	FOREIGN KEY (MedicineTypeId) REFERENCES MedicineType(Id) 

)
