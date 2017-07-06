USE [PTO]
GO

/****** Object:  StoredProcedure [dbo].[SP_MasterCardUpdate]    Script Date: 7/6/2017 6:04:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[SP_MasterCardUpdate]
As
Begin
	UPDATE MasterCard 
	SET DB_CR_INDICATOR = '1' WHERE DB_CR_INDICATOR = 'D'

	UPDATE MasterCard 
	SET DB_CR_INDICATOR = '2' WHERE DB_CR_INDICATOR = 'C'

	UPDATE MasterCard 
	SET DATE_OF_CHARGE = MERCATOR_KEY WHERE DATE_OF_CHARGE = ''

	UPDATE MasterCard 
	SET blank = NULL
End 
GO


