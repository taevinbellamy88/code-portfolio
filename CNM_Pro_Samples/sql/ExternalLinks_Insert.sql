USE [CnmPro]
GO

DECLARE @RC int
DECLARE @UserId int
DECLARE @UrlTypeId int
DECLARE @Url nvarchar(255)
DECLARE @EntityId int
DECLARE @EntityTypeId int
DECLARE @Id int

-- TODO: Set parameter values here.

EXECUTE @RC = [dbo].[ExternalLinks_Insert] 
   @UserId
  ,@UrlTypeId
  ,@Url
  ,@EntityId
  ,@EntityTypeId
  ,@Id OUTPUT
GO

