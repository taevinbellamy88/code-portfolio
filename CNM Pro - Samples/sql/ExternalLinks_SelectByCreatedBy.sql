USE [CnmPro]
GO

DECLARE @RC int
DECLARE @UserId int

-- TODO: Set parameter values here.

EXECUTE @RC = [dbo].[ExternalLinks_SelectByCreatedBy] 
   @UserId
GO

