USE [CnmPro]
GO

DECLARE @RC int
DECLARE @ReferenceTypeId int
DECLARE @UserId int

-- TODO: Set parameter values here.

EXECUTE @RC = [dbo].[SiteReferences_Insert] 
   @ReferenceTypeId
  ,@UserId
GO

