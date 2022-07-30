USE [CnmPro]
GO

DECLARE @RC int
DECLARE @Id int
DECLARE @UserId int
DECLARE @UrlTypeId int
DECLARE @Url nvarchar(255)
DECLARE @EntityId int
DECLARE @EntityTypeId int

-- TODO: Set parameter values here.

EXECUTE @RC = [dbo].[ExternalLinks_Update] 
   @Id
  ,@UserId
  ,@UrlTypeId
  ,@Url
  ,@EntityId
  ,@EntityTypeId
GO

