
-- =============================================
-- Author:		Ala
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[r_Asset_List] 
	-- Add the parameters for the stored procedure here
	@pageSize int, 
	@PageNumber int,
	@Id  nvarchar(50)  =null,
    @Name nvarchar(50) = null,
    @Status int = null,
    @TagId nvarchar(10) =null,
	@From datetime =null,
	@To datetime =null,
    @SortColumn int = null,
	@SortDirection int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT DISTINCT dbo.Asset.Id AS Id, 
		   dbo.Asset.AssetId AS AssetId, 
		   dbo.Asset.Name AS AssetName, 
		   dbo.Asset.Status AS AssetStatus, 
		   dbo.Asset.TagId AS TagId, 
		   dbo.Asset.CreatedDate AS [Date_and_Time], 
		   dbo.AssetImage.Path,
		   dbo.Category.Name,
		   dbo.Department.Name,
		   dbo.Type.Name,
		   dbo.Asset.Cost,
		   dbo.Asset.PurchaseDate,
		   dbo.Location.Name
	FROM   dbo.Asset 
			JOIN  dbo.Category ON dbo.Asset.CategoryId = dbo.Category.Id 	
			JOIN  dbo.Department ON dbo.Asset.DepartmentId = dbo.Department.Id 	
			JOIN  dbo.Type ON dbo.Asset.TypeId = dbo.Type.Id 	
			JOIN  dbo.Location ON dbo.Asset.LocationId = dbo.Location.Id 	
		   LEFT JOIN  dbo.AssetImage ON dbo.Asset.Id = dbo.AssetImage.AssetId 
	WHERE (@Id IS NULL OR dbo.Asset.AssetId=@Id)
	  AND (@Name IS NULL OR dbo.Asset.Name LIKE @Name+'%')
	  AND (@Status IS NULL OR dbo.Asset.Status=@Status)
	  AND (@TagId IS NULL OR dbo.Asset.TagId LIKE @TagId+'%')
	  AND (@From IS NULL OR dbo.Asset.CreatedDate >= @From)
	  AND (@To IS NULL OR dbo.Asset.CreatedDate <= @To)
	ORDER BY dbo.Asset.Id
	OFFSET ( @pageSize * (@PageNumber - 1)) ROWS
	FETCH NEXT @pageSize ROWS ONLY;
	--IF @SortDirection = 0
	--BEGIN
	--	SELECT dbo.Asset.Id AS AssetId, 
	--		   dbo.Asset.Name AS AssetName, 
	--		   dbo.Asset.Status AS AssetStatus, 
	--		   dbo.Asset.TagId AS TagId, 
	--		   dbo.Asset.CreatedDate AS [Date_and_Time], 
	--		   dbo.AssetImage.Path,
	--		   dbo.Category.Name,
	--		   dbo.Department.Name,
	--		   dbo.Type.Name,
	--		   dbo.Asset.Cost,
	--		   dbo.Asset.PurchaseDate
	--	FROM   dbo.Asset 
	--			JOIN  dbo.Category ON dbo.Asset.CategoryId = dbo.Category.Id 	
	--			JOIN  dbo.Department ON dbo.Asset.DepartmentId = dbo.Department.Id 	
	--			JOIN  dbo.Type ON dbo.Asset.TypeId = dbo.Type.Id 	
	--		   LEFT JOIN  dbo.AssetImage ON dbo.Asset.Id = dbo.AssetImage.AssetId 
	--	WHERE (@Id IS NULL OR dbo.Asset.Id=@Id)
	--	  AND (@Name IS NULL OR dbo.Asset.Name LIKE @Name+'%')
	--	  AND (@Status IS NULL OR dbo.Asset.Status=@Status)
	--	  AND (@TagId IS NULL OR dbo.Asset.TagId LIKE @TagId+'%')
	--	  AND (@From IS NULL OR dbo.Asset.CreatedDate >= @From)
	--	  AND (@To IS NULL OR dbo.Asset.CreatedDate <= @To)
	--	ORDER BY 
	--	CASE  
	--		WHEN @SortColumn=0 THEN dbo.Asset.Id
	--		WHEN @SortColumn=1 THEN dbo.Asset.Name
	--		WHEN @SortColumn=2 THEN dbo.Asset.Status
	--		WHEN @SortColumn=3 THEN dbo.Asset.TagId
	--		WHEN @SortColumn=4 THEN dbo.Asset.CreatedDate  
	--	END ASC
	--	OFFSET (@PageNumber - 1) ROWS
	--	FETCH NEXT @pageSize ROWS ONLY;
	--END
	--ELSE
	--BEGIN
	--	SELECT dbo.Asset.Id AS AssetId, 
	--		   dbo.Asset.Name AS AssetName, 
	--		   dbo.Asset.Status AS AssetStatus, 
	--		   dbo.Asset.TagId AS TagId, 
	--		   dbo.Asset.CreatedDate AS [Date_and_Time], 
	--		   dbo.AssetImage.Path,
	--		   dbo.Category.Name,
	--		   dbo.Department.Name,
	--		   dbo.Type.Name,
	--		   dbo.Asset.Cost,
	--		   dbo.Asset.PurchaseDate
	--	FROM   dbo.Asset 
	--			JOIN  dbo.Category ON dbo.Asset.CategoryId = dbo.Category.Id 	
	--			JOIN  dbo.Department ON dbo.Asset.DepartmentId = dbo.Department.Id 	
	--			JOIN  dbo.Type ON dbo.Asset.TypeId = dbo.Type.Id 	
	--		   LEFT JOIN  dbo.AssetImage ON dbo.Asset.Id = dbo.AssetImage.AssetId  
	--	WHERE (@Id IS NULL OR dbo.Asset.Id=@Id)
	--	  AND (@Name IS NULL OR dbo.Asset.Name LIKE @Name+'%')
	--	  AND (@Status IS NULL OR dbo.Asset.Status=@Status)
	--	  AND (@TagId IS NULL OR dbo.Asset.TagId LIKE @TagId+'%')
	--	  AND (@From IS NULL OR dbo.Asset.CreatedDate >= @From)
	--	  AND (@To IS NULL OR dbo.Asset.CreatedDate <= @To)
	--	ORDER BY 
	--	CASE  
	--		WHEN @SortColumn=0 THEN dbo.Asset.Id
	--		WHEN @SortColumn=1 THEN dbo.Asset.Name
	--		WHEN @SortColumn=2 THEN dbo.Asset.Status
	--		WHEN @SortColumn=3 THEN dbo.Asset.TagId
	--		WHEN @SortColumn=4 THEN dbo.Asset.CreatedDate  
	--	END DESC
	--	OFFSET (@PageNumber - 1) ROWS
	--	FETCH NEXT @pageSize ROWS ONLY;
	--END

	SELECT  COUNT(1)  as 'RowsCount'
	FROM   dbo.Asset LEFT JOIN  dbo.AssetImage ON dbo.Asset.Id = dbo.AssetImage.AssetId 
		WHERE (@Id IS NULL OR dbo.Asset.AssetId=@Id)
		  AND (@Name IS NULL OR dbo.Asset.Name LIKE @Name+'%')
		  AND (@Status IS NULL OR dbo.Asset.Status=@Status)
		  AND (@TagId IS NULL OR dbo.Asset.TagId LIKE @TagId+'%')
		  AND (@From IS NULL OR dbo.Asset.CreatedDate >= @From)
		  AND (@To IS NULL OR dbo.Asset.CreatedDate <= @To)

END
