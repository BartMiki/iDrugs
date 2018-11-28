CREATE PROCEDURE [dbo].[DeleteOrder]
	@orderId int = 0
AS
	DECLARE @errorMsg NVARCHAR(50)
	BEGIN TRY
		BEGIN TRANSACTION
			
			IF EXISTS (SELECT * FROM [Order] WHERE Id = @orderId AND SendOrderDate IS NOT NULL)
			BEGIN
				SET @errorMsg = 'Nie można usunąć zrealizowanych zamówień'
				RAISERROR(@errorMsg, 16, 1)
			END

			DELETE FROM OrderItem WHERE OrderId = @orderId

			DELETE FROM [Order] WHERE Id = @orderId

		COMMIT TRAN
		RETURN
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0 -- If Commit occured, then we need to rollback
			ROLLBACK TRAN

		IF LEN(@errorMsg) = 0
			SET @errorMsg = 'Wystąpił nieznany błąd - usuwanie zamówienia przerwane'

		RAISERROR(@errorMsg, 16, 1)
		RETURN

	END CATCH
RETURN
