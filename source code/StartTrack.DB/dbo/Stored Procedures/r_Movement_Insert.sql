-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [r_Movement_Insert] 
@TID int,
@AntinnaId int,
@Direction char(3),
@Status int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @AssetId int =(SELECT [Id] FROM [Asset] WHERE TagId=@TID)
	DECLARE @LocationId int =(SELECT [LocationId] FROM [Antinna] WHERE Id=@AntinnaId)

INSERT INTO [dbo].[Movement]
           ([AssetId]
           ,[LocationId]
           ,[Direction]
           ,[Status])
     VALUES
           (@AssetId
           ,@LocationId
           ,@Direction
           ,@Status)
END