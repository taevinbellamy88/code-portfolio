USE [CnmPro]
GO

DECLARE @RC int
DECLARE @PageIndex int
DECLARE @PageSize int

-- TODO: Set parameter values here.

EXECUTE @RC = [dbo].[SiteReferences_SelectAll] 
   @PageIndex
  ,@PageSize
GO

