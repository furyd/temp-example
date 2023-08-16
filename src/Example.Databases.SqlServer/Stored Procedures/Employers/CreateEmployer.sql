CREATE PROCEDURE [dbo].[CreateEmployer]
	@Name		NVARCHAR(128)
AS
	INSERT INTO [dbo].[Employers] ([Name]) VALUES (@Name)

	SELECT [Id]
		FROM [dbo].[Employers]
		WHERE [RowId] = SCOPE_IDENTITY()