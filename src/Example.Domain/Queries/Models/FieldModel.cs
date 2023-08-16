namespace Example.Domain.Queries.Models;

public readonly record struct FieldModel(Guid Id, string Name, string Type, IEnumerable<FieldOptionModel>? Values);