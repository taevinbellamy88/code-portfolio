USE [CnmPro]
GO

DECLARE @RC int
DECLARE @Id int
DECLARE @UserId int

-- TODO: Set parameter values here.

EXECUTE @RC = [dbo].[ExternalLinks_DeleteById] 
   @Id
  ,@UserId
GO

