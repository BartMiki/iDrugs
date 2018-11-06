CREATE PROCEDURE [dbo].[InsertApothecary]
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@MonthlySalary MONEY = 1499.99
AS
	IF NOT @MonthlySalary BETWEEN 500.00 AND 5000.00
	BEGIN
		RAISERROR('Pensja dla nowego aptekarza musi mieścić się pomiędzy 500.00 zł, al 5000.00 zł',16,1)
		RETURN
	END
	INSERT INTO Apothecary(FirstName, LastName, MonthlySalary)
	VALUES(@FirstName, @LastName, @MonthlySalary)