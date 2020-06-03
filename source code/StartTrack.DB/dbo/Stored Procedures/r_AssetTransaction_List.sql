-- =============================================
-- Author:		Ala
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[r_AssetTransaction_List] 
	-- Add the parameters for the stored procedure here
	@pageSize int, 
	@PageNumber int,
	@Id nvarchar(50) =null,
    @Name nvarchar(50) = null,
    @Status int = null,
    @Location int =null,
    @Direction varchar(10) = null,
	@From datetime =null,
	@To datetime =null,
    @SortColumn int = null,
	@SortDirection int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


	 
		SELECT dbo.Asset.AssetId AS AssetId, 
			   dbo.Asset.Name AS AssetName, 
			   dbo.Asset.Status AS AssetStatus, 
			   dbo.Location.Name AS Location, 
			   dbo.Movement.Date AS [Date_and_Time], 
			   dbo.Movement.Direction
		FROM   dbo.Asset INNER JOIN  dbo.Movement ON dbo.Asset.Id = dbo.Movement.AssetId 
						 INNER JOIN  dbo.Location ON dbo.Movement.LocationId = dbo.Location.Id
		WHERE (@Id IS NULL OR dbo.Asset.AssetId=@Id)
		  AND (@Name IS NULL OR dbo.Asset.Name LIKE @Name+'%')
		  AND (@Status IS NULL OR dbo.Asset.Status=@Status)
		  AND (@Location IS NULL OR dbo.Location.Id=@Location)
		  AND (@Direction IS NULL OR dbo.Movement.Direction=@Direction)
		  AND (@From IS NULL OR dbo.Movement.Date >= @From)
		  AND (@To IS NULL OR dbo.Movement.Date <= @To)
		ORDER BY Asset.Id desc
		OFFSET ( @pageSize * (@PageNumber - 1)) ROWS
		FETCH NEXT @pageSize ROWS ONLY;

	SELECT COUNT(1)  as 'RowsCount'
	FROM   dbo.Asset INNER JOIN  dbo.Movement ON dbo.Asset.Id = dbo.Movement.AssetId 
					 INNER JOIN  dbo.Location ON dbo.Movement.LocationId = dbo.Location.Id
	WHERE (@Id IS NULL OR dbo.Asset.AssetId=@Id)
		  AND (@Name IS NULL OR dbo.Asset.Name LIKE @Name+'%')
		  AND (@Status IS NULL OR dbo.Asset.Status=@Status)
		  AND (@Location IS NULL OR dbo.Location.Id=@Location)
		  AND (@Direction IS NULL OR dbo.Movement.Direction=@Direction)
		  AND (@From IS NULL OR dbo.Movement.Date >= @From)
		  AND (@To IS NULL OR dbo.Movement.Date <= @To)

END
