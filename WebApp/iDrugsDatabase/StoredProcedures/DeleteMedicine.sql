CREATE PROCEDURE [dbo].[DeleteMedicine]
	@id int
AS
	DELETE FROM Medicine WHERE Id = @id
RETURN
