CREATE PROCEDURE [dbo].[CreateOption]
		@Field			UNIQUEIDENTIFIER
	,	@Text			NVARCHAR(128)
	,	@Value			NVARCHAR(128)
AS
	
	DECLARE @FieldRowId	INT

	SELECT @FieldRowId = [RowId]
		FROM [dbo].[Fields]
		WHERE [Id] = @Field

	INSERT INTO [dbo].[Options] ([Field], [Text], [Value]) VALUES (@FieldRowId, @Text, @Value)

	SELECT [Id]
		FROM [dbo].[Options]
		WHERE [RowId] = SCOPE_IDENTITY()