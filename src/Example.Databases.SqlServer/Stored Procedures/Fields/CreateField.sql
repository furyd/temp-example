CREATE PROCEDURE [dbo].[CreateField]
		@Employer		UNIQUEIDENTIFIER
	,	@FieldType		UNIQUEIDENTIFIER
	,	@Text			NVARCHAR(128)
AS
	DECLARE @FieldTypeId	INT
	DECLARE @EmployerId		INT

	SELECT @FieldTypeId = [RowId]
		FROM [dbo].[FieldTypes]
		WHERE [Id] = @FieldType

	SELECT @EmployerId = [RowId]
		FROM [dbo].[Employers]
		WHERE [Id] = @Employer

	INSERT INTO [dbo].[Fields] ([Employer], [FieldType], [Text]) VALUES (@EmployerId, @FieldTypeId, @Text)

	SELECT [Id]
		FROM [dbo].[Fields]
		WHERE [RowId] = SCOPE_IDENTITY()