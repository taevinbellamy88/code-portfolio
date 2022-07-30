USE [CnmPro]
GO

DECLARE @RC int

-- TODO: Set parameter values here.

EXECUTE @RC = [dbo].[UrlTypes_SelectAll] 
GO

