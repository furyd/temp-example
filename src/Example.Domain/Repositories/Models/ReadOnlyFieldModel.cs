namespace Example.Domain.Repositories.Models;

public record ReadOnlyFieldModel(Guid Id, string Name, string Type)
{
    public IDictionary<string, string?> Values { get; } = new Dictionary<string, string?>();
}