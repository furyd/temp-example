namespace Example.Domain.Repositories.Implementation.DTOs;

public record FieldDTO(Guid FieldId, string FieldText, string FieldType, string? Text, string? Value, int TotalRows, int Page, int PageSize);