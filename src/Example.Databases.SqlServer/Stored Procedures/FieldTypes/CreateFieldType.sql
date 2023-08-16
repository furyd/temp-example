CREATE PROCEDURE [dbo].[CreateFieldType]
	@Name		NVARCHAR(128)
AS
	INSERT INTO [dbo].[FieldTypes] ([Name]) VALUES (@Name)

	SELECT [Id]
		FROM [dbo].[FieldTypes]
		WHERE [RowId] = SCOPE_IDENTITY()