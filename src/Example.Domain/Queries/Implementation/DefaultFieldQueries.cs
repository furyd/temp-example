using Example.Domain.Queries.Interfaces;
using Example.Domain.Queries.Models;
using Example.Domain.Repositories.Interfaces;
using Example.Domain.Shared.Interfaces;
using Example.Domain.Shared.Models;

namespace Example.Domain.Queries.Implementation;

public class DefaultFieldQueries : IFieldQueries
{
    private readonly IFieldRepository _fieldRepository;

    public DefaultFieldQueries(IFieldRepository fieldRepository)
    {
        _fieldRepository = fieldRepository;
    }

    public async Task<IPager<FieldModel>> List(PagedEmployerFilter filter)
     => Map(await _fieldRepository.List(filter));

    private static FieldModel Map(Repositories.Models.ReadOnlyFieldModel model)
        => new (model.Id, model.Name, model.Type, model.Values.Any() ? model.Values.Select(item => new FieldOptionModel(item.Key, item.Value)) : null);

    private static IPager<FieldModel> Map(IPager<Repositories.Models.ReadOnlyFieldModel> model)
        => new Pager<FieldModel>(model.Page, model.PageSize, model.Records.Select(Map).ToList());
}