CREATE PROCEDURE [dbo].[ListFieldsFilteredByEmployer]
		@Employer			UNIQUEIDENTIFIER
	,	@Page				INT
	,	@PageSize			INT
AS

DECLARE @EmployerId		INT

	SELECT @EmployerId = [RowId]
		FROM [dbo].[Employers]
		WHERE [Id] = @Employer

;WITH Data_CTE 
AS
(
    SELECT [Fields].[Id] as [FieldId], [Fields].[Text] as [FieldText], [Fields].[Employer], [FieldTypes].[Name] as [FieldType], [Fields].[RowId] FROM [Fields]
	INNER JOIN FieldTypes ON [Fields].[FieldType] = [FieldTypes].[RowId]
	WHERE [Fields].[Employer] = @EmployerId
), 
Count_CTE 
AS 
(
    SELECT COUNT(*) AS TotalRows FROM Data_CTE
),
Paged_CTE
AS(
	SELECT [FieldId], [RowId], [FieldText], [FieldType], [TotalRows], @PageSize AS [PageSize], @Page AS [Page]
	FROM Data_CTE
	CROSS JOIN Count_CTE
	ORDER BY [FieldId]--, [Options].[RowId]
	OFFSET (@Page - 1) * @PageSize ROWS
	FETCH NEXT @PageSize ROWS ONLY
)
--[FieldId], [FieldText], [FieldType], [Text], [Value], [TotalRows], @PageSize AS [PageSize], @Page AS [Page]
SELECT [Paged_CTE].[FieldId], [Paged_CTE].[FieldText], [Paged_CTE].[FieldType], [Options].[Text], [Options].[Value], [Paged_CTE].[TotalRows], [Paged_CTE].[PageSize], [Paged_CTE].[Page]
FROM Paged_CTE
LEFT JOIN [Options] ON [Options].[Field] = [Paged_CTE].[RowId]
CROSS JOIN Count_CTE
ORDER BY [FieldId], [Options].[RowId]